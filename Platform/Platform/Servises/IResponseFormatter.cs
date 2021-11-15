using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Platform.Servises
{
    public interface IResponseFormatter
    {
        Task Format(HttpContext context, string content);

        bool RichOutput => false;
    }
}
