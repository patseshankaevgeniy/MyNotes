using Application.Common.Interfaces;
using Application.TelegramBot.Auth.Models;
using AutoMapper;
using Domain.Entities.NewFolder;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TelegramBot.Auth.Commands;

public sealed record CreateTelegramAuthCodeCommand(Guid? UserId) : IRequest<TelegramAuthCodeModel>;

public sealed class CreateTelegramAuthCodeCommandValidator : AbstractValidator<CreateTelegramAuthCodeCommand>
{
    public CreateTelegramAuthCodeCommandValidator()
    {
        RuleFor(cmd => cmd.UserId).NotEmpty();
    }
}

public sealed class CreateTelegramAuthCodeCommandHandler : IRequestHandler<CreateTelegramAuthCodeCommand, TelegramAuthCodeModel>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _db;
    private readonly IGuidService _guidService;
    private readonly IDateTimeService _dateTimeService;

    public CreateTelegramAuthCodeCommandHandler(
        IMapper mapper,
        IApplicationDbContext db,
        IGuidService guidService,
        IDateTimeService dateTimeService)
    {
        _mapper = mapper;
        _db = db;
        _guidService = guidService;
        _dateTimeService = dateTimeService;
    }

    public async Task<TelegramAuthCodeModel> Handle(CreateTelegramAuthCodeCommand command, CancellationToken token)
    {
        var telegramAuthCode = new TelegramAuthCode
        {
            Id = _guidService.NewGuid(),
            UserId = command.UserId!.Value,
            LinkCode = $"{Guid.NewGuid()}",
            ShortCode = new Random().Next(1_000, 9_999),
            ShortExpirationDate = _dateTimeService.UtcNow.AddHours(2),
        };

        _db.TelegramAuthCodes.Add(telegramAuthCode);
        await _db.SaveChangesAsync(token);

        return _mapper.Map<TelegramAuthCodeModel>(telegramAuthCode);
    }
}