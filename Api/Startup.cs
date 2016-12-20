using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ReleaseNotesGenerator.Core;
using ReleaseNotesGenerator.Dal;
using AutoMapper;
using ReleaesNotesGenerator.Common.Enums;
using ReleaseNotesGenerator.Core.RepositoryHandlers;

namespace ReleaseNotesGenerator
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddDbContext<ReleaseNotesContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ReleaseNotesGenerator")));

            services.AddTransient<IProjectComponent, ProjectComponent>();
            services.AddTransient<IRepositoryComponent, RepositoryComponent>();
            services.AddTransient<IReleaseNotesComponent, ReleaseNotesComponent>();

            RepositoryFactory<IRepositoryHandler>.Register(RepositoryType.Git, () => new GitRepositoryHandler());
            RepositoryFactory<IRepositoryHandler>.Register(RepositoryType.Tfs, () => new TfsRepositoryHandler());

            services.AddOptions();
            services.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
