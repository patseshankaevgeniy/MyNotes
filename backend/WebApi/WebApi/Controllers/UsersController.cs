using Application.Users.Commands;
using Application.Users.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Models.Auth;
using WebApi.Models.Common;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace WebApi.Controllers;

[ApiController]
[Route("api/users")]
[Produces("application/json")]
public class UsersController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public UsersController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet("current", Name = "GetCurrentUser")]
    [ProducesResponseType(Status200OK, Type = typeof(UserDto))]
    [ProducesResponseType(Status401Unauthorized, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status404NotFound, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
    public async Task<ActionResult<UserDto>> GetCurrentUserAsync()
    {
        var query = new GetCurrentUserQuery();
        var model = await _mediator.Send(query);

        return Ok(_mapper.Map<UserDto>(model));
    }

    [HttpGet(Name = "GetUsers")]
    [ProducesResponseType(Status200OK, Type = typeof(IEnumerable<UserDto>))]
    [ProducesResponseType(Status401Unauthorized, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersAsync([FromQuery] SearchUsersOptionsDto searchOptions)
    {
        var query = new GetUsersQuery
        {
            HasMembers = searchOptions.HasMembers,
            SearchPattern = searchOptions.SearchPattern,
            ExcludeCurrent = searchOptions.ExcludeCurrent,
            OnlyCurrentUserMembers = searchOptions.OnlyCurrentUserMembers
        };
        var models = await _mediator.Send(query);

        return Ok(_mapper.Map<IEnumerable<UserDto>>(models));
    }

    [HttpPut(Name = "UpdateUser")]
    [ProducesResponseType(Status204NoContent)]
    [ProducesResponseType(Status401Unauthorized, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status404NotFound, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
    public async Task<ActionResult> UpdateUserAsync(UserDto user)
    {
        var command = new UpdateUserCommand
        {
            FirstName = user.FirstName,
            SecondName = user.SecondName,
            LanguageId = user.LanguageId,
            Email = user.Email,
        };

        await _mediator.Send(command);

        return NoContent();
    }
}
