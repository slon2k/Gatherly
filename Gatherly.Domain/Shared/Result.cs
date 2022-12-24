namespace Gatherly.Domain.Shared;

public record struct Result<TValue>
{
    private readonly TValue? value = default;
    
    private readonly List<Error>? errors = null;

    private static readonly Error NoErrors = Error.Failure("General.NoErrors", "The result is successful, no errors present");
    
    public bool IsError { get; }

    public List<Error> Errors => errors ?? new List<Error> { NoErrors };

    public TValue Value => value!;

    public Error FirstError
    {
        get
        {
            if (errors is null || errors.Count == 0)
            {
                return NoErrors;
            }

            return errors[0];
        }
    }

    public static Result<TValue> From(List<Error> errors)
    {
        return errors;
    }

    private Result(Error error)
    {
        errors = new List<Error> { error };
        IsError= true;
    }

    private Result(IEnumerable<Error> errors)
    {
        this.errors = new List<Error>();
        this.errors.AddRange(errors);
        IsError= true;
    }

    private Result(TValue value)
    {
        this.value = value;
        IsError= false;
    }

    public static implicit operator Result<TValue>(TValue value)
    {
        return new Result<TValue>(value);
    }

    public static implicit operator Result<TValue>(Error error)
    {
        return new Result<TValue>(error);
    }

    public static implicit operator Result<TValue>(List<Error> errors)
    {
        return new Result<TValue>(errors);
    }

    public static implicit operator Result<TValue>(Error[] errors)
    {
        return new Result<TValue>(errors);
    }

    public TResult Match<TResult>(Func<TValue, TResult> onValue, Func<List<Error>, TResult> onError)
    {
        if (IsError)
        {
            return onError(Errors);
        }

        return onValue(Value);
    }
}
