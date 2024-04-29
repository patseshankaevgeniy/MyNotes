using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Members.Commands
{
    public sealed record CreateMemberCommand(Guid? UserId) : IRequest<Unit>;

    public sealed class CreateMemberCommandValidator : AbstractValidator<CreateMemberCommand>
    {
        public CreateMemberCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }

    public sealed class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, Unit>
    {
        private readonly IApplicationDbContext _db;
        private readonly ICurrentUserService _currentUserService;

        public CreateMemberCommandHandler(ICurrentUserService currentUserService, IApplicationDbContext db)
        {
            _currentUserService = currentUserService;
            _db = db;
        }

        public async Task<Unit> Handle(CreateMemberCommand command, CancellationToken cancellationToken)
        {
            var invitedUser = await _db.Users
                .Include(u => u.MembershipStatus)
                .FirstAsync(x => x.Id == command.UserId, cancellationToken);

            if (invitedUser == null)
            {
                throw new NotFoundException(nameof(invitedUser), command.UserId);
            }

            if (invitedUser.GroupId != null)
            {
                throw new BadRequestException("Invited user already has a group");
            }

            var currentUser = await _db.Users
                .Include(u => u.MembershipStatus)
                .FirstOrDefaultAsync(u => u.Id == _currentUserService.UserId);

            if (currentUser == null)
            {
                throw new NotFoundException(nameof(currentUser), _currentUserService.UserId);
            }

            if (currentUser.Group != null)
            {
                throw new BadRequestException("Current user cant invite more than one user");
            }

            currentUser.MembershipStatus = new MembershipStatus
            {
                IsInviter = true,
                Status = AcceptanceStatus.Pending
            };

            invitedUser.MembershipStatus = new MembershipStatus
            {
                IsInviter = false,
                Status = AcceptanceStatus.Pending
            };

            var group = new Group
            {
                Name = $"{currentUser.FirstName} | {invitedUser.SecondName}",
                Users = new List<User> { currentUser, invitedUser }
            };

            _db.Groups.Add(group);
            await _db.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
