using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AcadaAcademy.Services;
using Microsoft.Extensions.Configuration;
using AcadaAcademy.DataModels;
using AcadaAcademy.ViewModels;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net;

namespace LostZone
{
    public class Startup
    {
        private IHostingEnvironment _env;
        private IConfiguration _config;

        public Startup(IConfiguration config, IHostingEnvironment env)
        {
            _config = config;
            _env = env;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_config);
            if (_env.IsEnvironment("Development") || _env.IsEnvironment("Testing"))
            {
                services.AddScoped<IMailService, DebugMailService>();
            }
            else
            {
                //Implement mail service here
            }

            services.AddDbContext<AcadaContext>(config =>
            {
                config.UseSqlServer(_config.GetConnectionString("AcadaConnection"));
            });

            services.AddTransient<GeoService>();
            services.AddScoped<IAcadaRepository, AcadaRepository>();
            services.AddTransient<AcadaContextSeedData>();
            services.AddLogging();

            services.AddMvc(config =>
            {
                if (_env.IsProduction())
                {
                    Console.WriteLine("Entering Production");
                    config.Filters.Add(new RequireHttpsAttribute());    // Redirect to https. We don't want to send anything not protected over the wire! 
                    
                }
            }).AddJsonOptions(config =>
            {
                config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddIdentity<AcadaUser, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 8;
                
            })
            .AddEntityFrameworkStores<AcadaContext>(); //Store identity entities in LostZoneContext

            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = ctx =>
                {
                    //context.Response.StatusCode = 401;
                    if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == (int)HttpStatusCode.OK)
                    {
                        ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    }
                    else
                    {
                        // Default behaviour
                        ctx.Response.Redirect(ctx.RedirectUri);
                    }


                    return Task.CompletedTask;
                };
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            Mapper.Initialize(config =>

            {
                config.CreateMap<ItemViewModel, Item>().ReverseMap();
                config.CreateMap<ItemRouteViewModel, ItemRoute>().ReverseMap();
            });
            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                loggerFactory.AddDebug(LogLevel.Information);
            }
            else
            {
                loggerFactory.AddDebug(LogLevel.Error);
            }

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=App}/{action=Index}/{id?}");
            });

        }
    }
}
