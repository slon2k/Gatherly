using Gatherly.Domain.Shared;

namespace Gatherly.Domain.Errors;

public static class Member
{
    public static Error NotFound => Error.NotFound("Member.NotFound", "Member was not found");

    public static Error DuplicateEmail => Error.Conflict("Member.DuplicateEmail", "Email is not unique");
}
