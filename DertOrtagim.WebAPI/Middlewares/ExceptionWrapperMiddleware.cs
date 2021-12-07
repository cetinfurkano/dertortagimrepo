using Core.Constants;
using Core.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DertOrtagim.WebAPI.Middlewares
{
    public class ExceptionWrapperMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionWrapperMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception ex)
            {
                var response = new ErrorResult(Messages.InternalServerError);

                var jsonResponseData = JsonConvert.SerializeObject(response);

                httpContext.Response.Clear();
                httpContext.Response.StatusCode = 500;

                await httpContext.Response.WriteAsync(jsonResponseData);
            }
        }
    }

    public static class ExceptionWrapperMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionWrapperMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionWrapperMiddleware>();
        }
    }
}
