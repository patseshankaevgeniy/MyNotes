using Application.Common.Interfaces;
using Application.Members.Models;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Members.Queries
{
    public sealed class GetMembersQuery : IRequest<IEnumerable<MemberModel>>;

    public sealed class GetMembersQueryHandler : IRequestHandler<GetMembersQuery, IEnumerable<MemberModel>>
    {
        private readonly IApplicationDbContext _db;
        private readonly ICurrentUserService _currentUserService;
        public GetMembersQueryHandler(
            IApplicationDbContext db,
            ICurrentUserService currentUserService)
        {
            _db = db;
            _currentUserService = currentUserService;
        }

        public async Task<IEnumerable<MemberModel>> Handle(GetMembersQuery query, CancellationToken token)
        {
            var users = await _db.Groups
                .AsNoTracking()
                .Where(g => g.Users.Any(u => u.Id == _currentUserService.UserId))
                .SelectMany(g => g.Users)
                .Include(u => u.MembershipStatus)
                .ToListAsync(token);

            if (!users.Any())
            {
                return Enumerable.Empty<MemberModel>();
            }

            if (users.Count == 1)
            {
                throw new Exception($"There are not users in group {users.First().GroupId}");
            }

            var members = new List<MemberModel>();

            foreach (var user in users)
            {
                if (user.Id != _currentUserService.UserId)
                {
                    MemberStatus status;
                    if (user.MembershipStatus!.Status == AcceptanceStatus.Completed)
                    {
                        status = MemberStatus.Approved;
                    }
                    else
                    {
                        status = user.MembershipStatus!.IsInviter!.Value
                           ? MemberStatus.RequiredApproval
                           : MemberStatus.WaitForApproval;
                    }
                    members.Add(new MemberModel
                    {
                        Id = user.MembershipStatus!.Id,
                        UserId = user.Id,
                        FirstName = user.FirstName,
                        SecondName = user.SecondName,
                        Email = user.Email,
                        //ImageId = user.ImageId,
                        Status = status
                    });
                }
            }

            return members;
        }
    }
}
