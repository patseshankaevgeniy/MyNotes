using Application.TelegramBot.Common.Services.Interfaces;
using Application.TelegramBot.TextMessages.Models;
using Application.TelegramBot.TextMessages.TextDecompositions.Commands;
using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TelegramBot.TextMessages;

public sealed record HandlerTextMessageUpdateCommand(string TextMessage) : IRequest;

public sealed class HandlerTextMessageUpdateCommandValidator : AbstractValidator<HandlerTextMessageUpdateCommand>
{
    public HandlerTextMessageUpdateCommandValidator()
    {
        RuleFor(cmd => cmd.TextMessage).NotEmpty();
    }
}

public sealed class HandlerTextMessageUpdateCommandHandler : IRequestHandler<HandlerTextMessageUpdateCommand>
{
    private readonly IMediator _mediator;
    private readonly IEnumerable<IBotCommandSelector> _selectors;

    public HandlerTextMessageUpdateCommandHandler(
        IMediator mediator,
        IEnumerable<IBotCommandSelector> selectors)
    {
        _mediator = mediator;
        _selectors = selectors;
    }

    public async Task<Unit> Handle(HandlerTextMessageUpdateCommand command, CancellationToken token)
    {
        var messageDecompasition = await _mediator.Send(new DecomposeTextMessageCommand(command.TextMessage));

        var selectionResults = new List<SelectionResult>();

        foreach (var selector in _selectors)
        {
            selectionResults.Add(await selector.AnalyzeAsync(messageDecompasition));
        }

        var targetSelection = selectionResults
            .Where(x => x.Suits)
            .MaxBy(x => x.CompatibilityInPercent);

        await _mediator.Send(targetSelection!.BotCommand!);

        return Unit.Value;
    }
}