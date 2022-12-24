namespace Gatherly.Domain.Enums;

public enum ErrorType
{
    None = 0,
    Validation = 1,
    Forbidden = 403,
    NotFound = 404,
    Conflict = 409,
    Unexpected = 500,
}
