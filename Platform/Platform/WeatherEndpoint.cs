using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Platform.Servises;
using Microsoft.Extensions.DependencyInjection;

namespace Platform
{
    public class WeatherEndpoint
    {
        //private IResponseFormatter formatter;

        //public WeatherEndpoint(IResponseFormatter formatter)
        //{
        //    this.formatter = formatter;
        //}

        public async Task Endpoint(HttpContext context, IResponseFormatter formatter)
        {
            await formatter.Format(context, "Middleware Class: It is  cloudy in Milan");
        }
    }
}
