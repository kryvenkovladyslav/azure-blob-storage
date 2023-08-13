using Azure;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using WebApplication.Infrastructure.Common;

namespace WebApplication.Infrastructure.Middleware
{
    public sealed class ExceptionHandler
    {
        private readonly RequestDelegate next;

        public ExceptionHandler(RequestDelegate next)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception exception)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                if(exception.GetType() == typeof(RequestFailedException))
                {
                    response.StatusCode = StatusCodes.Status403Forbidden;
                    response.Redirect(UrlConstants.AccessDeniedUri);
                    return;
                }

                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Redirect(UrlConstants.InternalErrorUri);
            }
        }
    }
}