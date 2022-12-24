using Gatherly.Application.Members.Commands;
using Gatherly.Application.Members.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gatherly.Web.Controllers
{
    public class MembersController : ApiControllerBase
    {
        private readonly ISender mediator;

        public MembersController(ISender mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("members")]
        public async Task<IActionResult> GetMembers()
        {
            var result = await mediator.Send(new GetMembersQuery());

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(result.Error.Message);
        }

        [HttpPost("members")]
        public async Task<IActionResult> CreateMember(CreateMemberCommand command)
        {
            var result = await mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(result.Error.Message);
        }
    }
}
