using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Platform.Servises;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.FileProviders;

namespace Platform
{
    public class Startup
    {
        private IConfiguration Configuration { get; set; }

        public Startup(IConfiguration config)
        {
            Configuration = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MessageOptions>(Configuration.GetSection("Location"));
            services.Configure<CookiePolicyOptions>(opts => {
                opts.CheckConsentNeeded = context => true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCookiePolicy();
            app.UseMiddleware<ConsentMiddleware>();

            //app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider($"{env.ContentRootPath}/staticfiles"),
                RequestPath = "/files"
            });

            app.UseRouting();

            app.UseMiddleware<LocationMiddleware>();

            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/config")
                {
                    string defaultDebug = Configuration["Logging:LogLevel:Default"];
                    await context.Response.WriteAsync($"The config setting is: {defaultDebug}");

                    string environ = Configuration["ASPNETCORE_ENVIRONMENT"];
                    await context.Response.WriteAsync($"\nThe env setting is: {environ}");

                    string wsId = Configuration["WebService:Id"];
                    string wsKey = Configuration["WebService:Key"];
                    await context.Response.WriteAsync($"\nThe env secret Id is: {wsId}");
                    await context.Response.WriteAsync($"\nThe env secret Key is: {wsKey}");
                }
                else
                {
                    await next();
                }
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/cookie", async context =>
                {
                    int counter1 = int.Parse(context.Request.Cookies["counter1"] ?? "0") + 1;
                    context.Response.Cookies.Append("counter1", counter1.ToString(), new CookieOptions { 
                        MaxAge = TimeSpan.FromMinutes(30),
                        IsEssential = true
                    });

                    int counter2 = int.Parse(context.Request.Cookies["counter2"] ?? "0") + 1;
                    context.Response.Cookies.Append("counter2", counter2.ToString(), new CookieOptions { MaxAge = TimeSpan.FromMinutes(30) });

                    await context.Response.WriteAsync($"Counter1: {counter1},\nCounter2: {counter2}");
                });

                endpoints.MapGet("/clear", context =>
                {
                    context.Response.Cookies.Delete("counter1");
                    context.Response.Cookies.Delete("counter2");

                    context.Response.Redirect("/");
                    return Task.CompletedTask;
                });

                endpoints.MapGet("/", async context =>
                {
                    logger.LogDebug("Response for / started");
                    await context.Response.WriteAsync("Hello  World!");
                    logger.LogDebug("Response for / completed");
                });

                endpoints.MapFallback(async context =>
                {
                    await context.Response.WriteAsync("Hello  World...");
                });
            });
        }
    }
}
