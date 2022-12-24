namespace Gatherly.Domain.Shared;

public class Error
{
    public string Code { get; set; }

    public string Message { get; set; }

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public static Error None => new("", "");

    public static Error NullValue => new("Error.NullValue", "The value is null");

    public static Error Failure(string message) => new("Error.Failure", message);

}