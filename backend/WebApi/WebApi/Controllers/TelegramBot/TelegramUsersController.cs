using Application.TelegramBot.TelegramUsers.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Models.Common;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Expenses.WebAPI.Controllers.TelegramBot;

[Authorize]
[ApiController]
[Route("api/telegram-users")]
[Produces("application/json")]
public sealed class TelegramUsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public TelegramUsersController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    // Please do like this if you want to be "Shit Coder" :)
    [HttpPost(Name = "CheckTelegramUserExists")]
    [ProducesResponseType(Status200OK, Type = typeof(bool))]
    [ProducesResponseType(Status401Unauthorized, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
    public async Task<ActionResult<bool>> CheckTelegramUserExistsAsync()
    {
        var query = new CheckTelegramUserExistsQuery();
        var telegramUserExists = await _mediator.Send(query);

        return Ok(telegramUserExists);
    }

    [HttpDelete(Name = "DeleteTelegramUser")]
    [ProducesResponseType(Status204NoContent)]
    [ProducesResponseType(Status400BadRequest, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status401Unauthorized, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
    public async Task<ActionResult> DeleteTelegramUserAsync()
    {
        var command = new DeleteTelegramUserCommand();
        await _mediator.Send(command);

        return NoContent();
    }
}