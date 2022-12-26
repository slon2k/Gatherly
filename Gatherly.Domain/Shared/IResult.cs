namespace Gatherly.Domain.Shared
{
    public interface IResult
    {
        List<Error> Errors { get; }
        
        Error FirstError { get; }
        
        bool IsFailure { get; }
        
        bool IsSuccess { get; }
    }


    public interface IResult<TValue> : IResult
    {
        TValue Value { get; }

        TResult Match<TResult>(Func<TValue, TResult> onValue, Func<List<Error>, TResult> onError);
    }
}