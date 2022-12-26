namespace Gatherly.Domain.Shared;
using static Domain.Errors.Result;

public readonly record struct Result<TValue> : IResult<TValue>
{
    private readonly TValue? value = default;

    private readonly List<Error>? errors = null;

    public bool IsFailure => errors is not null;

    public bool IsSuccess => !IsFailure;

    public List<Error> Errors => errors ?? new List<Error> { NoErrors };

    public TValue Value => value!;

    public Error FirstError => (errors is null || errors.Count == 0) ? NoErrors : errors[0];

    public static Result<TValue> From(List<Error> errors) => errors;

    private Result(Error error)
    {
        errors = new List<Error> { error };
    }

    private Result(IEnumerable<Error> errors)
    {
        this.errors = new List<Error>(errors);
    }

    private Result(TValue value)
    {
        this.value = value;
    }

    public static implicit operator Result<TValue>(TValue value) => new(value);

    public static implicit operator Result<TValue>(Error error) => new(error);

    public static implicit operator Result<TValue>(List<Error> errors) => new(errors);

    public static implicit operator Result<TValue>(Error[] errors) => new(errors);

    public TResult Match<TResult>(Func<TValue, TResult> onSuccess, Func<List<Error>, TResult> onFailure) => IsFailure ? onFailure(Errors) : onSuccess(Value);
}

public readonly record struct Result : IResult
{
    private readonly List<Error>? errors = null;

    public bool IsFailure => errors is not null;

    public bool IsSuccess => errors is null;

    public List<Error> Errors => errors ?? new List<Error> { NoErrors };

    public Error FirstError => (errors is null || errors.Count == 0) ? NoErrors : errors[0];

    public static Result From(List<Error> errors) => new(errors);

    public static Result Success() => new();

    private Result(Error error)
    {
        errors = new List<Error> { error };
    }

    private Result(IEnumerable<Error> errors)
    {
        this.errors = new List<Error>(errors);
    }

    public static implicit operator Result(Error error) => new(error);

    public static implicit operator Result(List<Error> errors) => new(errors);

    public static implicit operator Result(Error[] errors) => new(errors);

    public TResult Match<TResult>(Func<TResult> onSuccess, Func<List<Error>, TResult> onFailure) => IsFailure ? onFailure(Errors) : onSuccess();
}