using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Movie.Services;
using Movie.Repositories;
using AutoMapper;
using Movie.Web.Extensions;

namespace Movie.Web
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();

            services.AddCustomSwagger();

            services.AddControllersWithViews();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            // Database Connection String
            if (_env.IsDevelopment())
            {
                // In Memory
                services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("AppDbContext"));
            }
            else
            {
                // Sql Server
                services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("SqlDbContext")));
            }

            // Configure Auto Mapper
            services.AddAutoMapper(typeof(Startup));

            // Dependency Injection Modules
            ConfigureDependencyInjection(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCustomSwagger();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }

        public void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<ISaveRepository, SaveRepository>();

            services.AddScoped<IGenresService, GenresService>();
            services.AddScoped<IGenresRepository, GenresRepository>();

            services.AddScoped<IMoviesService, MoviesService>();
            services.AddScoped<IMoviesRepository, MoviesRepository>();
        }
    }
}
