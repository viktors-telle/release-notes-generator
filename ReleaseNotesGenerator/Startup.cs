using System;
using System.IO;
using System.Net;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ReleaseNotesGenerator.Components.Implementations;
using ReleaseNotesGenerator.Components.Implementations.ProjectTrackingToolHandlers;
using ReleaseNotesGenerator.Components.Implementations.RepositoryHandlers;
using ReleaseNotesGenerator.Components.Interfaces;
using ReleaseNotesGenerator.Dal;
using ReleaseNotesGenerator.Dto.Options;
using ReleaseNotesGenerator.Enums;
using ReleaseNotesGenerator.Middleware;
using Serilog;
using Serilog.Enrichers;
using Serilog.Formatting.Json;
using Serilog.Sinks.RollingFile;
using Swashbuckle.AspNetCore.Swagger;

namespace ReleaseNotesGenerator
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("ModifyRepository",
            //                      policy => policy.Requirements.Add(new RepositoryModificationRequirement()));
            //});


            services.AddDbContext<ReleaseNotesContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("ReleaseNotesGenerator")));

            services.AddScoped<IProjectComponent, ProjectComponent>();
            services.AddScoped<IRepositoryComponent, RepositoryComponent>();
            services.AddScoped<IReleaseNotesComponent, ReleaseNotesComponent>();
            services.AddScoped<IProjectTrackingToolComponent, ProjectTrackingToolComponent>();
            services.AddScoped<IEmailComponent, EmailComponent>();
            services.AddScoped<TfsHandler, TfsHandler>();
            services.AddScoped<JiraHandler, JiraHandler>();
            services.AddScoped<GitRepositoryHandler, GitRepositoryHandler>();
            services.AddScoped<TfsRepositoryHandler, TfsRepositoryHandler>();

            var serviceProvider = services.BuildServiceProvider();
            RepositoryFactory<IRepositoryHandler>.Register(RepositoryType.Git,
                () => serviceProvider.GetService<GitRepositoryHandler>());
            RepositoryFactory<IRepositoryHandler>.Register(RepositoryType.Tfs,
                () => serviceProvider.GetService<TfsRepositoryHandler>());
            RepositoryFactory<IRepositoryHandler>.Register(RepositoryType.GitHub,
                () => serviceProvider.GetService<GitHubRepositoryHandler>());

            ProjectTrackingToolFactory<IProjectTrackingToolHandler>.Register(ProjectTrackingToolType.Tfs,
                () => serviceProvider.GetService<TfsHandler>());
            ProjectTrackingToolFactory<IProjectTrackingToolHandler>.Register(ProjectTrackingToolType.Jira,
                () => serviceProvider.GetService<JiraHandler>());

            services.Configure<SmtpOptions>(Configuration.GetSection("Smtp"));

            services.AddOptions();
            services.AddAutoMapper();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Release Notes Generator API",
                    Description = "Release Notes Generator API Swagger Documentation",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Viktors Telle", Url = "https://github.com/viktors-telle" },
                    License = new License { Name = "MIT", Url = "https://en.wikipedia.org/wiki/MIT_License" }
                });
            });

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
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            //app.UseMiddleware<ApiAuthenticationMiddleware>();
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseStatusCodePages();
            app.MapWhen(x => !x.Request.Path.Value.StartsWith("/swagger", StringComparison.OrdinalIgnoreCase),
                builder =>
                {
                    builder.UseMvc(routes =>
                    {
                        routes.MapRoute(
                            name: "default",
                            template: "{controller=Home}/{action=Index}/{id?}");

                        routes.MapSpaFallbackRoute(
                            name: "spa-fallback",
                            defaults: new {controller = "Home", action = "Index"});
                    });
                });
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Release Notes Generator API V1");
            });

            ReleaseNotesContext.Migrate(app);
            ConfigureLogging();
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
