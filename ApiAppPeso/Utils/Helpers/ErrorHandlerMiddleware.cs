using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiAppPeso.Utils.Helpers
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                StandardResponseModel responses = new StandardResponseModel();
                switch (error)
                {
                    case AppException e:
                        // custom application error
                        responses.StatusCode = 400;
                        responses.Message = (new { error?.Message }).Message.ToString();
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        responses.StatusCode = 404;
                        responses.Message = (new { error?.Message }).Message.ToString();
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        responses.StatusCode = 500;
                        responses.Message = (new { error?.Message }).Message.ToString();
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                var result = JsonSerializer.Serialize(responses);
                await response.WriteAsync(result);
            }
        }
    }
}
