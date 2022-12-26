using Gatherly.Application.Members.Commands;
using Gatherly.Application.Members.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gatherly.Web.Controllers
{
    public class MembersController : ApiControllerBase
    {
        public MembersController(ISender sender) : base(sender)
        {
        }

        [HttpGet("members")]
        public async Task<IActionResult> GetMembers()
        {
            var result = await sender.Send(new GetMembersQuery());

            return result.Match(
                result => Ok(result),
                errors => Problem(errors)
            );

        }

        [HttpPost("members")]
        public async Task<IActionResult> CreateMember(CreateMemberCommand command)
        {
            var result = await sender.Send(command);

            return result.Match(
                result => Ok(result),
                errors => Problem(errors)
            );
        }
    }
}
