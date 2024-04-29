using Application.Members.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models.Common;
using WebApi.Models;
using System.Linq;
using static Microsoft.AspNetCore.Http.StatusCodes;
using Application.Members.Commands;
using WebApi.Models.Auth;
using System;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Produces("application/json")]
    [Route("api/members")]
    public class MembersController: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public MembersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }


        [HttpGet(Name = "GetMembers")]
        [ProducesResponseType(Status200OK, Type = typeof(IEnumerable<MemberDto>))]
        [ProducesResponseType(Status401Unauthorized, Type = typeof(ErrorDto))]
        [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetMembersAsync()
        {
            var query = new GetMembersQuery();
            var models = await _mediator.Send(query);

            return Ok(models.Select(_mapper.Map<MemberDto>));
        }

        [HttpPost(Name = "CreateMember")]
        [ProducesResponseType(Status204NoContent)]
        [ProducesResponseType(Status400BadRequest, Type = typeof(ErrorDto))]
        [ProducesResponseType(Status401Unauthorized, Type = typeof(ErrorDto))]
        [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
        public async Task<ActionResult> CreateMemberAsync(CreateMemberDto dto)
        {
            var command = new CreateMemberCommand(dto.UserId);
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPatch(Name = "AcceptInvitation")]
        [ProducesResponseType(Status204NoContent)]
        [ProducesResponseType(Status400BadRequest, Type = typeof(ErrorDto))]
        [ProducesResponseType(Status401Unauthorized, Type = typeof(ErrorDto))]
        [ProducesResponseType(Status404NotFound, Type = typeof(ErrorDto))]
        [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
        public async Task<ActionResult> AcceptInvitationAsync()
        {
            var command = new AcceptInvitationCommand();
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id:guid}", Name = "DeleteMember")]
        [ProducesResponseType(Status204NoContent)]
        [ProducesResponseType(Status400BadRequest, Type = typeof(ErrorDto))]
        [ProducesResponseType(Status401Unauthorized, Type = typeof(ErrorDto))]
        [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
        public async Task<ActionResult> DeleteMemberAsync(Guid id)
        {
            var command = new DeleteMemberCommand(id);
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
