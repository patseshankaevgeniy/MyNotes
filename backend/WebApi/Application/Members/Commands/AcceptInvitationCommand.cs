using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Members.Commands
{
    public record AcceptInvitationCommand : IRequest<Unit>;

    public sealed class AcceptInvitationCommandHandler : IRequestHandler<AcceptInvitationCommand, Unit>
    {
        private readonly IApplicationDbContext _db;
        private readonly IMessageService _messageService;
        private readonly ICurrentUserService _currentUserService;

        public AcceptInvitationCommandHandler(
            ICurrentUserService currentUserService,
            IMessageService messageService,
            IApplicationDbContext db)
        {
            _currentUserService = currentUserService;
            _messageService = messageService;
            _db = db;
        }

        public async Task<Unit> Handle(AcceptInvitationCommand command, CancellationToken cancellationToken)
        {
            var currentUser = await _db.Users
                .Include(u => u.Group)
                .Include(u => u.MembershipStatus)
                .FirstAsync(u => u.Id == _currentUserService.UserId);

            if (currentUser is null)
            {
                throw new NotFoundException(nameof(currentUser), _currentUserService.UserId);
            }

            if (currentUser.MembershipStatus is not { Status: AcceptanceStatus.Pending })
            {
                throw new BadRequestException("Can't accept user without invitation");
            }

            var inviter = await _db.Users
                .Include(u => u.Group)
                .Include(u => u.MembershipStatus)
                .FirstAsync(u => u.GroupId == currentUser.GroupId && u.Id != currentUser.Id, cancellationToken);

            currentUser.MembershipStatus!.IsInviter = null;
            currentUser.MembershipStatus!.Status = AcceptanceStatus.Completed;

            inviter.MembershipStatus!.IsInviter = null;
            inviter.MembershipStatus!.Status = AcceptanceStatus.Completed;

            await _db.SaveChangesAsync(cancellationToken);

            await _messageService.SendUserMemberAcceptedAsync(currentUser.Id);

            return Unit.Value;
        }
    }
}
