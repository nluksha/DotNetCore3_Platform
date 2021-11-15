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

        public WeatherMiddleware(RequestDelegate nextDelegate)
        {
            next = nextDelegate;
            // this.formatter = formatter;
        }

        public async Task Invoke(HttpContext context, IResponseFormatter formatter1, IResponseFormatter formatter2, IResponseFormatter formatter3)
        {
            if (context.Request.Path == "/middleware/class")
            {
                await formatter1.Format(context, String.Empty);
                await formatter2.Format(context, String.Empty);
                await formatter3.Format(context, String.Empty);
            } else
            {
                await next(context);
            }
        }
    }
}
