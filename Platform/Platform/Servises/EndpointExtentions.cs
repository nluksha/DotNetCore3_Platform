using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Platform.Servises;

namespace Microsoft.AspNetCore.Builder
{
    public static class EndpointExtentions
    {
        public static void MapEndpoint<T>(this IEndpointRouteBuilder app, string path, string methodName = "Endpoint")
        {
            var methodInfo = typeof(T).GetMethod(methodName);

            if (methodInfo == null || methodInfo.ReturnType != typeof(Task))
            {
                throw new System.Exception("Method cannot be used");
            }

            T endpointInstance = ActivatorUtilities.CreateInstance<T>(app.ServiceProvider);

            ParameterInfo[] methodParams = methodInfo.GetParameters();
            app.MapGet(path, context => (Task)methodInfo.Invoke(
                endpointInstance,
                methodParams.Select(p => p.ParameterType == typeof(HttpContext)
                ? context
                : context.RequestServices.GetService(p.ParameterType)).ToArray()
                ));
        }
    }
}
