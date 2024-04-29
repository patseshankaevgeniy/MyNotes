using Application.Common.Exceptions;
using Application.Common.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Members.Commands
{
    public sealed record DeleteMemberCommand(Guid? MemberId) : IRequest<Unit>;

    public sealed class DeleteMemberCommandValidator : AbstractValidator<DeleteMemberCommand>
    {
        public DeleteMemberCommandValidator()
        {
            RuleFor(t => t.MemberId).NotEmpty();
        }
    }

    public sealed class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand, Unit>
    {
        private readonly IApplicationDbContext _db;
        private readonly IMessageService _messageService;
        private readonly ICurrentUserService _currentUserService;

        public DeleteMemberCommandHandler(
            IApplicationDbContext db,
            IMessageService messageService,
            ICurrentUserService currentUserService)
        {
            _db = db;
            _messageService = messageService;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(DeleteMemberCommand command, CancellationToken token)
        {
            var group = await _db.Groups
                .Include(g => g.Users).ThenInclude(u => u.MembershipStatus)
                .Where(g => g.Users.Any(u => u.MembershipStatus!.Id == command.MemberId))
                .FirstOrDefaultAsync(token);

            if (group == null)
            {
                throw new NotFoundException(nameof(group), command.MemberId!);
            }

            foreach (var user in group.Users)
            {
                _db.MembershipStatuses.Remove(user.MembershipStatus!);
            }

            _db.Groups.Remove(group);

            await _db.SaveChangesAsync(token);

            await _messageService.SendUserMemberDeclinedAsync(_currentUserService.UserId);

            return Unit.Value;
        }
    }
}
