using El_Master.API.Responses;
using El_Master.Application.Common.Results;
using El_Master.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace El_Master.API.Extensions
{
    public static class ResultExtensions
    {
        public static IActionResult ToApiResponse<T>(this Result<T> result)
        {
            var response = new ApiResponse<T>
            {
                Success = result.IsSuccess,
                Message = result.Message,
                Data = result.Value,
                Errors = result.Error is null ? null : new List<string> { result.Error }
            };

            return result.Status switch
            {
                ResultStatus.Success => new OkObjectResult(response),

                ResultStatus.BadRequest => new BadRequestObjectResult(response),

                ResultStatus.NotFound => new NotFoundObjectResult(response),

                ResultStatus.Unauthorized => new UnauthorizedObjectResult(response),

                ResultStatus.Forbidden => new ObjectResult(response)
                {
                    StatusCode = StatusCodes.Status403Forbidden
                },

                _ => new ObjectResult(response)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                }
            };
        }
    }
}
