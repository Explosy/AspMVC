using System;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using AspAPI.Models;

namespace AspAPI.Handlers {
  public class ExceptionHandlerMiddleware {
    private readonly RequestDelegate next;
    public ExceptionHandlerMiddleware(RequestDelegate next) {
      this.next = next;
    }

    public async Task Invoke(HttpContext context) {
      try {
        await next.Invoke(context);
      } catch (Exception exception) {
        await HandleExceptionMessageAsync(context, exception).ConfigureAwait(false);
      }
    }

    private Task HandleExceptionMessageAsync(HttpContext context, Exception exception) {
      int statusCode = (int)HttpStatusCode.InternalServerError;
      string result = JsonConvert.SerializeObject(new ResponseModel<object>() {
        IsSuccess = false,
        Error = exception.Message,
        StackTrace = exception.StackTrace
      });
      context.Response.ContentType = MediaTypeNames.Application.Json;
      context.Response.StatusCode = statusCode;
      return context.Response.WriteAsync(result);
    }
  }
}
