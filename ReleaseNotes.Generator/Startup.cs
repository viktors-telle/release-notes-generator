using System.IO;
using System.Net;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ReleaseNotes.Generator.Components.Implementations;
using ReleaseNotes.Generator.Components.Implementations.Authorization;
using ReleaseNotes.Generator.Components.Interfaces;
using ReleaseNotes.Generator.Components.Interfaces.Authorization;
using ReleaseNotes.Generator.Controllers;
using ReleaseNotes.Generator.Dal;
using ReleaseNotes.Generator.Dto.Options;
using ReleaseNotes.Generator.Enums;
using ReleaseNotes.Generator.Middleware;
using Serilog;
using Serilog.Enrichers;
using Serilog.Formatting.Json;
using Serilog.Sinks.RollingFile;

namespace ReleaseNotes.Generator
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            ConfigureLogging();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddDbContext<ReleaseNotesContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("ReleaseNotesGenerator")));

            services.AddTransient<IProjectComponent, ProjectComponent>();
            services.AddTransient<IRepositoryComponent, RepositoryComponent>();
            services.AddTransient<IBranchComponent, BranchComponent>();
            services.AddTransient<IReleaseNotesComponent, ReleaseNotesComponent>();
            services.AddTransient<IProjectTrackingToolComponent, ProjectTrackingToolComponent>();
            services.AddTransient<IRepositoryItemPathComponent, RepositoryItemPathComponent>();
            services.AddTransient<IEmailComponent, EmailComponent>();
            services.AddTransient<TfsHandler, TfsHandler>();
            services.AddTransient<JiraHandler, JiraHandler>();
            services.AddTransient<GitRepositoryHandler, GitRepositoryHandler>();
            services.AddTransient<TfsRepositoryHandler, TfsRepositoryHandler>();
            services.AddTransient<IRepositoryAuthorizationComponent, RepositoryAuthorizationComponent>();
            services.AddTransient<IBranchAuthorizationComponent, BranchAuthorizationComponent>();
            services.AddTransient<IRepositoryItemPathAuthorizationComponent, RepositoryItemPathAuthorizationComponent>();

            var serviceProvider = services.BuildServiceProvider();
            RepositoryFactory<IRepositoryHandler>.Register(RepositoryType.Git,
                () => serviceProvider.GetService<GitRepositoryHandler>());
            RepositoryFactory<IRepositoryHandler>.Register(RepositoryType.Tfs,
                () => serviceProvider.GetService<TfsRepositoryHandler>());

            ProjectTrackingToolFactory<IProjectTrackingToolHandler>.Register(ProjectTrackingToolType.Tfs,
                () => serviceProvider.GetService<TfsHandler>());
            ProjectTrackingToolFactory<IProjectTrackingToolHandler>.Register(ProjectTrackingToolType.Jira,
                () => serviceProvider.GetService<JiraHandler>());

            services.Configure<SmtpOptions>(Configuration.GetSection("Smtp"));

            services.AddOptions();
            services.AddAutoMapper();
            services.AddSwaggerGen();

            ServicePointManager.DefaultConnectionLimit = 200;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
        {
            loggerFactory.AddSerilog();
            appLifetime.ApplicationStopped.Register(Log.CloseAndFlush);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseMiddleware<ApiAuthenticationMiddleware>();
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseStatusCodePages();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUi();
            ReleaseNotesContext.Migrate(app);
        }

        private void ConfigureLogging()
        {
            var logDirectory = Configuration["Logging:ApplicationLogsFolder"];
            var applicationName = Configuration["Logging:ApplicationName"];
            var applicationVersion = Configuration["Logging:ApplicationVersion"];
            var environment = Configuration["Logging:Environment"];
            var logFileNameFormat = $"{applicationName}-{applicationVersion}-{{Date}}.log";
            var pathFormat = Path.Combine(logDirectory, logFileNameFormat);

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.With<MachineNameEnricher>()
                .Enrich.WithProperty("ApplicationName", applicationName)
                .Enrich.WithProperty("ApplicationVersion", applicationVersion)
                .Enrich.WithProperty("Environment", environment)
                .WriteTo.LiterateConsole()
                .WriteTo.Sink(new RollingFileSink(pathFormat, new JsonFormatter(renderMessage: true), null, 31,
                    Encoding.UTF8))
                .CreateLogger();
        }
    }
}