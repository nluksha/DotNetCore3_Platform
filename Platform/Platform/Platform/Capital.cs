using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Platform
{
    public static class Capital
    {
        public static async Task Endpoint(HttpContext context)
        {
            string capital = null;
            string country = context.Request.RouteValues["country"] as string;

            switch ((country ?? "").ToLower())
            {
                case "uk":
                    capital = "London";
                    break;
                case "paris":
                    capital = "Paris";
                    break;
                case "monaco":
                    var generator = context.RequestServices.GetService<LinkGenerator>();
                    var url = generator.GetPathByRouteValues(context, "population", new { city = country });

                    context.Response.Redirect(url);
                    return;
            }

            if (capital != null)
            {
                await context.Response.WriteAsync($"{capital} is the capital of {country}");
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
        }
    }
}
