using Application.TelegramBot.TextMessages.TextDecompositions.Models;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TelegramBot.TextMessages.TextDecompositions.Commands;

public sealed record DecomposeTextMessageCommand(string TextMessage) : IRequest<TextDecomposition>;

public sealed class DecomposeTextMessageCommandValidator : AbstractValidator<DecomposeTextMessageCommand>
{
    public DecomposeTextMessageCommandValidator()
    {
        RuleFor(command => command.TextMessage).NotEmpty();
    }
}

public sealed class DecomposeTextMessageCommandHandler : IRequestHandler<DecomposeTextMessageCommand, TextDecomposition>
{
    public Task<TextDecomposition> Handle(DecomposeTextMessageCommand command, CancellationToken token)
    {
        var items = new List<TextItem>();

        var spitedText = command.TextMessage!.Split(" ", StringSplitOptions.RemoveEmptyEntries)!;

        for (var index = 0; index < spitedText.Length; index++)
        {
            var itemValue = spitedText[index].Trim();

            if (decimal.TryParse(itemValue.Replace(",", "."), out var number))
            {
                items.Add(new(number.ToString(), TextItemType.Number, index));
            }
            else
            {
                items.Add(new(itemValue, TextItemType.Word, index));
            }
        }

        return Task.FromResult(new TextDecomposition
        {
            Items = items
        });
    }
}