using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Group> Groups { get; }
        DbSet<Language> Languages { get; }
        DbSet<MembershipStatus> MembershipStatuses { get; }
        DbSet<IdentityUserToken<Guid>> UserTokens { get; }

        void ClearTracking();
        Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}
