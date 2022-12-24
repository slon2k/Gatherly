namespace Gatherly.Domain.Shared;

public class Result
{
    public Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }

    public Error Error { get; }

    public bool IsFailure => !IsSuccess;

    public static Result Success() => new(true, Error.None);
}

public class Result<TValue>: Result
{
    private readonly TValue? value;

    protected internal Result(TValue? value, bool isSuccess, Error error) : base(isSuccess, error)
    {
        this.value = value;
    }

    public TValue Value => IsSuccess ? value! : throw new InvalidOperationException("The result is failure and the value does not exist");

    public static Result<TValue> Success(TValue value) => new(value, true, Error.None);

    public static Result<TValue> Failure(Error error) => new(default, false, error);

    public static implicit operator Result<TValue>(TValue? value) => new(value, true, Error.None);
}
