using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Platform
{
    public class Capital
    {
        private RequestDelegate next;

        public Capital()
        {
        }

        public Capital(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var parts = context.Request.Path.ToString()
                .Split("/", StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 2 && parts[0] == "capital")
            {
                string capital = null;
                string country = parts[1];

                switch (country.ToLower())
                {
                    case "uk":
                        capital = "London";
                        break;
                    case "paris":
                        capital = "Paris";
                        break;
                    case "monaco":
                        context.Response.Redirect($"/population/{country}");
                        return;
                }

                if (capital != null)
                {
                    await context.Response.WriteAsync($"{capital} is the capital of {country}");

                    return;
                }
            }

            if (next != null)
            {
                await next(context);
            }
        }
    }
}
