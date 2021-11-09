using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platform
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Map("/branch", branch =>
            {
                branch.UseMiddleware<QueryStringMiddleWare>();

                branch.Run(async (context) =>
                {
                    await context.Response.WriteAsync("Branch Middleware");
                });
            });


            app.Use(async (contect, next) =>
            {
                await next();
                await contect.Response.WriteAsync($"\nStatus Code: { contect.Response.StatusCode}");
            });

            app.Use(async (contect, next) =>
            {
                if (contect.Request.Path == "/short")
                {
                    await contect.Response.WriteAsync("Request Short Circuied");
                }
                else
                {
                    await next();
                }
            });

            app.Use(async (contect, next) =>
            {
                if (contect.Request.Method == HttpMethods.Get && contect.Request.Query["custom"] == "true")
                {
                    await contect.Response.WriteAsync("Custom Middlewere \n");
                }

                await next();
            });

            app.UseMiddleware<QueryStringMiddleWare>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
