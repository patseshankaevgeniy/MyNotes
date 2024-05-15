using Application.Auth.Commands;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Models.Auth;
using WebApi.Models.Common;
using WebApi.Models.TelegramBot;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/auth")]
[Produces("application/json")]
public sealed class AuthController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public AuthController(
        IMapper mapper,
        IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost("log-in", Name = "LogIn")]
    [ProducesResponseType(Status200OK, Type = typeof(LogInResultDto))]
    [ProducesResponseType(Status400BadRequest, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
    public async Task<ActionResult<LogInResultDto>> LogInAsync(LogInDto dto)
    {
        var command = new LogInCommand
        {
            Email = dto.Email,
            Password = dto.Password
        };

        var model = await _mediator.Send(command);
        return Ok(_mapper.Map<LogInResultDto>(model));
    }

    [AllowAnonymous]
    [HttpPost("log-in-with-telegram-user", Name = "LogInWithTelegramUser")]
    [ProducesResponseType(Status200OK, Type = typeof(LogInResultDto))]
    [ProducesResponseType(Status400BadRequest, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
    public async Task<ActionResult<LogInResultDto>> LogInWithTelegramUserAsync(TelegramUserLogInDto dto)
    {
        var command = new LoginWithTelegramUserCommand
        {
            TelegramUserId = dto.TelegramUserId
        };

        var model = await _mediator.Send(command);
        return Ok(_mapper.Map<LogInResultDto>(model));
    }

    [AllowAnonymous]
    [HttpPost("sign-up", Name = "SignUp")]
    [ProducesResponseType(Status200OK, Type = typeof(LogInResultDto))]
    [ProducesResponseType(Status400BadRequest, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
    public async Task<ActionResult<LogInResultDto>> SignUpAsync(SignUpDto dto)
    {
        var command = new SignUpCommand
        {
            Email = dto.Email,
            UserName = dto.UserName,
            Password = dto.Password
        };

        var model = await _mediator.Send(command);
        return Ok(_mapper.Map<LogInResultDto>(model));
    }
}