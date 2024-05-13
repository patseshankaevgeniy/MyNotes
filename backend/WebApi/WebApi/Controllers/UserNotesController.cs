using Application.UserNotes.Commands;
using Application.UserNotes.Queries;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Models.Common;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Produces("application/json")]
    [Route("api/user-notes")]
    public class UserNotesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UserNotesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetUserNotes")]
        [ProducesResponseType(Status200OK, Type = typeof(IEnumerable<UserNoteDto>))]
        [ProducesResponseType(Status401Unauthorized, Type = typeof(ErrorDto))]
        [ProducesResponseType(Status404NotFound, Type = typeof(ErrorDto))]
        [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
        public async Task<ActionResult<IEnumerable<UserNoteDto>>> GetUserNotesAsync()
        {
            var query = new GetUserNotesQuery();
            var models = await _mediator.Send(query);

            return Ok(_mapper.Map<IEnumerable<UserNoteDto>>(models));
        }

        [HttpPost(Name = "CreateUserNote")]
        [ProducesResponseType(Status200OK, Type = typeof(UserNoteDto))]
        [ProducesResponseType(Status400BadRequest, Type = typeof(ErrorDto))]
        [ProducesResponseType(Status401Unauthorized, Type = typeof(ErrorDto))]
        [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
        public async Task<ActionResult<UserNoteDto>> CreateUserNoteAsync(UserNoteDto dto)
        {
            var command = new CreateUserNoteCommand
            {
                CreatorId = dto.UserId,
                Priority = dto.Priority,
                Text = dto.Text,
                Сompletion = dto.Сompletion,
                Source = Source.WebApp
            };
            var model = await _mediator.Send(command);

            return Created($"api/user-notes/{model.Id}", _mapper.Map<UserNoteDto>(model));
        }

        [HttpDelete("{id:guid}", Name = "DeleteUserNote")]
        [ProducesResponseType(Status204NoContent)]
        [ProducesResponseType(Status400BadRequest, Type = typeof(ErrorDto))]
        [ProducesResponseType(Status401Unauthorized, Type = typeof(ErrorDto))]
        [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
        public async Task<ActionResult> DeleteGroupAsync(Guid id)
        {
            var command = new DeleteUserNoteCommand(id);
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
