using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Platform
{
    public class WeatherEndpoint
    {
        public static async Task Endpoint(HttpContext context)
        {
            await context.Response.WriteAsync("Middleware Class: It is  cloudy in Milan");
        }
    }
}
