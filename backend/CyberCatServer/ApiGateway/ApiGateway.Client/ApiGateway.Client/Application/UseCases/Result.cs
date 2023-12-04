using System;

namespace ApiGateway.Client.Application.UseCases
{
    public struct Result
    {
        public bool IsSuccess => string.IsNullOrEmpty(Error);
        public string Error { get; private set; }

        public static Result Success => new Result();

        public static Result Failure(string error) => new Result()
        {
            Error = error
        };

        public static Result Failure(Exception exception) => new Result()
        {
            Error = exception.Message
        };

        public static implicit operator Result(Exception exception) => Failure(exception);
    }

    public struct Result<TValue>
    {
        public bool IsSuccess => Value != null && string.IsNullOrEmpty(Error);
        public string Error { get; private set; }
        public TValue Value { get; private set; }

        public static Result<TValue> Success(TValue value) => new Result<TValue>()
        {
            Value = value
        };

        public static Result<TValue> Failure(string error) => new Result<TValue>()
        {
            Error = error
        };

        public static Result<TValue> Failure(Exception exception) => new Result<TValue>()
        {
            Error = exception.Message
        };

        public static implicit operator Result<TValue>(TValue value) => Success(value);
        public static implicit operator TValue(Result<TValue> result) => result.Value;
        public static implicit operator Result<TValue>(Exception exception) => Failure(exception);
    }
}