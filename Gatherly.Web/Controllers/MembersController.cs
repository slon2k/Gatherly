using Gatherly.Application.Members.Commands;
using Gatherly.Application.Members.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gatherly.Web.Controllers
{
    [Route("members")]
    public class MembersController : ApiControllerBase
    {
        public MembersController(ISender sender) : base(sender)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetMembers()
        {
            var result = await sender.Send(new GetMembersQuery());

            return result.Match(
                result => Ok(result),
                errors => Problem(errors)
            );
        }

        [HttpPost]
        public async Task<IActionResult> CreateMember(CreateMemberCommand command)
        {
            var result = await sender.Send(command);

            return result.Match(
                result => Ok(result),
                errors => Problem(errors)
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMemberById(Guid id)
        {
            var result = await sender.Send(new GetMemberByIdQuery(id));

            return result.Match(
                result => Ok(result),
                errors => Problem(errors)
            );
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMember(UpdateMemberCommand command)
        {
            var result = await sender.Send(command);

            return result.Match(
                result => Ok(result),
                errors => Problem(errors)
            );
        }
    }
}
