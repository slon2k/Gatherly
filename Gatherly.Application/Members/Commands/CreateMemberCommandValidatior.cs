using FluentValidation;
using Gatherly.Domain.Entities;

namespace Gatherly.Application.Members.Commands;

internal class CreateMemberCommandValidatior : AbstractValidator<CreateMemberCommand>
{
    public CreateMemberCommandValidatior()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(Member.EmailMaxLength);
        
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(Member.FirstNameMaxLength);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(Member.LastNameMaxLength);
    }
}
