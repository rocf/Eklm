using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Eklm.Core.Interfaces;
using Eklm.Infrastructure;
using Eklm.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Eklm.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private const string _defaultCorsPolicyName = "Eklm.Api.Cors";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            services.AddRouting(options => { options.LowercaseUrls = true; });
            services.AddCors(options => { options.AddPolicy(_defaultCorsPolicyName,
                builder =>
                {
                    builder.WithOrigins(Configuration["Application:CorsOrigins"]
                        .Split(",", StringSplitOptions.RemoveEmptyEntries).ToArray())
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            services.AddDbContext<EklmContext>(options => options.UseSqlite(Configuration.GetConnectionString("EklmContext")));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IHFuncRepository, HFuncRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStatusCodePages();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An Error Occurred.");
                    });
                });
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseCors(_defaultCorsPolicyName);
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });


            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    //spa.UseAngularCliServer(npmScript: "start");
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
            });
        }
    }
}
