using El_Master.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Common.Results
{
    public class Result<T>
    {
        public bool IsSuccess => Status == ResultStatus.Success;

        public ResultStatus Status { get; set; }

        public string? Message { get; set; }
        public string? Error { get; set; }

        public T? Value { get; set; }

        public static Result<T> Success(T value, string? message = null)
            => new Result<T>
            {
                Status = ResultStatus.Success,
                Value = value,
                Message= message ?? string.Empty
            };

        public static Result<T> Failure( string mesage, string? error = null)
            => new Result<T>
            {
                Status = ResultStatus.BadRequest,
                Message = mesage ?? string.Empty,
                Error = error
            };

        public static Result<T> NotFound(string mesage, string? error = null)
            => new Result<T>
            {
                Status = ResultStatus.NotFound,
                Message = mesage,
                Error = error
            };
    }
}
