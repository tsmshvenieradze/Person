using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Person.Domain;
using Person.Domain.Contracts;
using Person.Domain.Models.ExceptionModels;
using Person.Domain.Services.Models;

namespace Person.WebApi.Middlewares
{
    
    public class ExceptionLogMiddleware
    {  
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionLogMiddleware> _logger;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStringLocalizer<SharedResources> _sharedLocalizer;

        public ExceptionLogMiddleware(RequestDelegate next, ILoggerFactory loggerFactory,
            IConfiguration config,
            IHttpContextAccessor httpContextAccessor, IStringLocalizer<SharedResources> sharedLocalizer)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = loggerFactory?.CreateLogger<ExceptionLogMiddleware>() ??
                      throw new ArgumentNullException(nameof(loggerFactory));
            _config = config;
            _httpContextAccessor = httpContextAccessor;
            _sharedLocalizer = sharedLocalizer;
        }

        public async Task Invoke(HttpContext context, RepositoryProvider repositoryProvider)
        {
            try
            {
                await _next(context);
            }

           
            catch (DomainException ex)
            {
                var log = new ExceptionLog
                {
                    Metadata = context.Request.Path,
                    IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString()
                };

                var error = new  ServiceResult<bool> { Message = _sharedLocalizer[ex.Message], Status = ServiceResultStatus.Error};
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                var jsonString = JsonConvert.SerializeObject(error);

                await repositoryProvider.Logger.Log( log, error, context.Response.StatusCode, ex);

                await context.Response.WriteAsync(jsonString, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                var log = new ExceptionLog
                {
                    Message = FlattenException(ex),
                    Metadata = context.Request.Path,
                    IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString()
                };

                var error = new ServiceResult<bool> { Message = "Server error", Status = ServiceResultStatus.Error };
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                var jsonString = JsonConvert.SerializeObject(error);

                await repositoryProvider.Logger.Log(log, error, context.Response.StatusCode, ex);

                await context.Response.WriteAsync(jsonString, Encoding.UTF8);
            }
        }


        public static string FlattenException(Exception exception)
        {
            var stringBuilder = new StringBuilder();
            var deepSize = 50;
            var i = 0;
            while (exception != null && i < deepSize)
            {
                stringBuilder.AppendLine(exception.Message);
                stringBuilder.AppendLine(exception.StackTrace);

                exception = exception.InnerException;
                i++;
            }

            return stringBuilder.ToString();
        }
    }
}
