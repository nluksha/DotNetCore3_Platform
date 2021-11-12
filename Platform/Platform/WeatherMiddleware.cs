using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Platform
{
    public class WeatherMiddleware
    {
        private RequestDelegate next;

        public WeatherMiddleware(RequestDelegate nextDelegate)
        {
            next = nextDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/middleware/class")
            {
                await context.Response.WriteAsync("Middleware Class: It is  raining in London");
            } else
            {
                await next(context);
            }
        }
    }
}
