using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Platform.Servises;

namespace Microsoft.AspNetCore.Builder
{
    public static class EndpointExtentions
    {
        public static void MapWeather(this IEndpointRouteBuilder app, string path)
        {
            IResponseFormatter formatter = app.ServiceProvider.GetService<IResponseFormatter>();

            app.MapGet(path, context => Platform.WeatherEndpoint.Endpoint(context, formatter));
        }

    }
}
