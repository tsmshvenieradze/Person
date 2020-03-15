using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace Person.WebApi.Middlewares
{
    public static class ExceptionLogMiddlewareExtensions
    {
        public static IApplicationBuilder ExceptionLog(this IApplicationBuilder builder)
        { 
            return builder.UseMiddleware<ExceptionLogMiddleware>();
        }
    }
}
