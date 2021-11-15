using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Platform.Servises;

namespace Platform
{
    public class WeatherMiddleware
    {
        private RequestDelegate next;
        // private IResponseFormatter formatter;

        public WeatherMiddleware(RequestDelegate nextDelegate, IResponseFormatter formatter)
        {
            next = nextDelegate;
            // this.formatter = formatter;
        }

        public async Task Invoke(HttpContext context, IResponseFormatter formatter)
        {
            if (context.Request.Path == "/middleware/class")
            {
                await formatter.Format(context, "Middleware Class: It is  raining in London");
            } else
            {
                await next(context);
            }
        }
    }
}
