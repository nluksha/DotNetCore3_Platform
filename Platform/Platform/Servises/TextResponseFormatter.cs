using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platform.Servises
{
    public class TextResponseFormatter : IResponseFormatter
    {
        private int responseCounter = 0;

        public async Task Format(HttpContext context, string content)
        {
            await context.Response.WriteAsync($"Response {++responseCounter}:\n{content}");
        }
    }
}
