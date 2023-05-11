using $ext_safeprojectname$.Core.Enums;
using $ext_safeprojectname$.Core.Exceptions;
using $ext_safeprojectname$.SharedKernel.Extensions;
using Microsoft.AspNetCore.Http;
using NLog;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Dynamic;
using System.Collections.Generic;

namespace $ext_safeprojectname$.Web.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly Logger _logger;
        private readonly RequestDelegate _next;
        private List<int> AllowedCodes = new() { 400, 401, 403, 404 };

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.Clear();
            context.Response.ContentType = "application/json";

            _logger.Error(exception, $"{exception.Message}");

            if (exception is $ext_safeprojectname$Exception geralException)
            {
                var statusCode = AllowedCodes.Contains((int)geralException.DetailErrorCode) ? (int)geralException.DetailErrorCode : 400;
                await WriteResponse(context, statusCode, (int)geralException.DetailErrorCode, geralException.Message, geralException.ExtraData);
                return;
            }

            if (exception is AggregateException aggregate && aggregate.InnerExceptions.Any(x => x is $ext_safeprojectname$Exception))
            {
                var first = aggregate.InnerExceptions.Where(x => x is $ext_safeprojectname$Exception).First() as $ext_safeprojectname$Exception;
                var statusCode = AllowedCodes.Contains((int)first.DetailErrorCode) ? (int)first.DetailErrorCode : 400;
                await WriteResponse(context, statusCode, (int)first.DetailErrorCode, first.Message, first.ExtraData);
                return;
            }

            await WriteResponse(context, (int)HttpStatusCode.InternalServerError, (int)InternalErrorCode.InternalServerError, "Internal Server Error");
        }

        private async Task WriteResponse(HttpContext context, int statusCode, int detailErrorCode, string message, ExpandoObject extra = null)
        {
            context.Response.StatusCode = statusCode;

            var error = new ErrorDetails
            {
                StatusCode = detailErrorCode,
                Message = message,
                ExtraData = extra
            };

            await context.Response.WriteAsync(error.ToJson());
        }

        internal class ErrorDetails
        {
            public ErrorDetails()
            {
            }

            public int StatusCode { get; set; }
            public string Message { get; set; }
            public ExpandoObject ExtraData { get; set; }
        }
    }
}
