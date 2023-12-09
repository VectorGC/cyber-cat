using System;

namespace ApiGateway.Client.Application.UseCases
{
    public struct Result
    {
        public bool IsSuccess => string.IsNullOrEmpty(Error) && ErrorCode == ErrorCode.None;
        public ErrorCode ErrorCode { get; private set; }
        public string Error { get; private set; }

        public static Result Success => new Result();

        public static Result Failure(ErrorCode errorCode) => new Result()
        {
            ErrorCode = errorCode,
            Error = errorCode.Message()
        };

        public static Result Failure(Exception exception) => new Result()
        {
            ErrorCode = ErrorCode.Exception,
            Error = exception.Message
        };

        public static implicit operator Result(Exception exception) => Failure(exception);
    }

    public struct Result<TValue>
    {
        public bool IsSuccess => Value != null && string.IsNullOrEmpty(Error) && ErrorCode == ErrorCode.None;
        public ErrorCode ErrorCode { get; private set; }
        public string Error { get; private set; }
        public TValue Value { get; private set; }

        public static Result<TValue> Success(TValue value) => new Result<TValue>()
        {
            Value = value
        };

        public static Result<TValue> Failure(ErrorCode errorCode) => new Result<TValue>()
        {
            ErrorCode = errorCode,
            Error = errorCode.Message()
        };

        public static Result<TValue> Failure<TSource>(Result<TSource> result)
        {
            if (result.IsSuccess)
                throw new InvalidOperationException(nameof(result));

            return new Result<TValue>()
            {
                ErrorCode = result.ErrorCode,
                Error = result.Error
            };
        }

        public static Result<TValue> Failure(Exception exception) => new Result<TValue>()
        {
            ErrorCode = ErrorCode.Exception,
            Error = exception.Message
        };

        public static implicit operator Result<TValue>(TValue value) => Success(value);
        public static implicit operator TValue(Result<TValue> result) => result.Value;
        public static implicit operator Result<TValue>(Exception exception) => Failure(exception);
    }
}