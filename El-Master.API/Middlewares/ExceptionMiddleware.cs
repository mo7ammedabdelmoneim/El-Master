using El_Master.API.Responses;
using El_Master.Application.Common.Exceptions;
using System.Text.Json;

namespace El_Master.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                var response = new ApiResponse<object>
                {
                    Success = false,
                    Message = "Validation Failed",
                    Data = null,
                    Errors = ex.Errors
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new ApiResponse<object>
                {
                    Success = false,
                    Message = "Internal Server Error",
                    Data = null,
                    Errors = new List<string> { ex.Message }
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
