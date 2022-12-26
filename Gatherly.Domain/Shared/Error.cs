using Gatherly.Domain.Enums;

namespace Gatherly.Domain.Shared;

public class Error
{
    public string Code { get; }

    public ErrorType Type { get; }

    public string Message { get; }

    private Error(ErrorType type, string code, string message)
    {
        Type = type;
        Code = code;
        Message = message;
    }

    public static Error None(string code, string message) => new(ErrorType.None, code, message);

    public static Error Validation(string code, string message) => new(ErrorType.Validation, code, message);

    public static Error Failure(string code, string message) => new(ErrorType.Unexpected, code, message);

    public static Error Forbidden(string code, string message) => new(ErrorType.Forbidden, code, message);

    public static Error NotFound(string code, string message) => new(ErrorType.NotFound, code, message);

    public static Error Conflict(string code, string message) => new(ErrorType.Conflict, code, message);
}