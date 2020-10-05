using GameScope.Infra.Common.Exceptions;
using GameScope.Infra.Common.Logging;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GameScope.Api.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILoggerService logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            object errors = null;
            var location = context.GetEndpoint().DisplayName;
            switch (exception)
            {
                case GameScopeException gameScopeException:
                    _logger.LogError($"{location}-{gameScopeException.Code}-{gameScopeException.Message}");
                    errors = gameScopeException.Message;
                    context.Response.StatusCode = 400;
                    break;
                case { } e:
                    _logger.LogError($"{location}-{e.Message}");
                    errors = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";
            if (errors != null)
            {
                var result = JsonConvert.SerializeObject(new
                {
                    errors
                });
                await context.Response.WriteAsync(result);
            }
        }
    }
}
