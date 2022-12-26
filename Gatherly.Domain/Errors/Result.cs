using Gatherly.Domain.Shared;

namespace Gatherly.Domain.Errors;

public static class Result
{
    public static Error NoErrors => Error.None("Result.NoErrors", "No errors. The result is successful.");
}
