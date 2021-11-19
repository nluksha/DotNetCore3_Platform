using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.AspNetCore.Routing;
using Platform.Servises;

namespace Platform
{
    public class SumEndpoint
    {
        public async Task Endpoint(HttpContext context, IDistributedCache cache, IResponseFormatter formatter, LinkGenerator generator)
        {
            int count = int.Parse((string)context.Request.RouteValues["count"]);
            long total = 0;
            for (int i = 0; i < count; i++)
            {
                total += 1;
            }

            string totalString = $"{DateTime.Now.ToLongTimeString()} {total}";

            context.Response.Headers["Cache-Control"] = "public, max-age=120";

            string url = generator.GetPathByRouteValues(context, null, new { count = count });

            await formatter.Format(context, $"<div>({DateTime.Now.ToLongTimeString()}) Total for {count}"
                + $" values:</div><div>{totalString}</div>"
                + $"<a href={url}>Reload</a>"
                );
        }
    }
}
