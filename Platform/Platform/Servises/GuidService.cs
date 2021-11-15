using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platform.Servises
{
    public class GuidService: IResponseFormatter
    {
        private Guid guid = Guid.NewGuid();

        public async Task Format(HttpContext context, string content)
        {
            await context.Response.WriteAsync($"Guid: {guid}\n{content}");
        }
    }
}
