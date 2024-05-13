using Application.TelegramBot.Auth.Commands;
using Application.TelegramBot.Auth.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Models.Common;
using WebApi.Models.TelegramBot;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace WebApi.Controllers.TelegramBot;

[Authorize]
[ApiController]
[Produces("application/json")]
[Route("api/telegram-auth-codes")]
public sealed class TelegramAuthCodesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public TelegramAuthCodesController(
        IMapper mapper,
        IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet(Name = "GetTelegramAuthCode")]
    [ProducesResponseType(Status200OK, Type = typeof(TelegramAuthCodeDto))]
    [ProducesResponseType(Status400BadRequest, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status401Unauthorized, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
    public async Task<ActionResult<TelegramAuthCodeDto>> GetTelegramAuthCodeAsync()
    {
        var query = new GetTelegramAuthCodeQuery();
        var model = await _mediator.Send(query);

        return Ok(_mapper.Map<TelegramAuthCodeDto>(model));
    }

    [HttpPut(Name = "RefreshTelegramAuthCode")]
    [ProducesResponseType(Status200OK, Type = typeof(TelegramAuthCodeDto))]
    [ProducesResponseType(Status400BadRequest, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status401Unauthorized, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
    public async Task<ActionResult<TelegramAuthCodeDto>> RefreshTelegramAuthCodeAsync()
    {
        var query = new RefreshTelegramAuthCodeCommand();
        var model = await _mediator.Send(query);

        return Ok(_mapper.Map<TelegramAuthCodeDto>(model));
    }
}