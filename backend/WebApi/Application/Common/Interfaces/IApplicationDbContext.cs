using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Threading;
using Domain.Entities.NewFolder;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Group> Groups { get; }
        DbSet<UserNote> UserNotes { get; }
        DbSet<Language> Languages { get; }
        DbSet<MembershipStatus> MembershipStatuses { get; }
        DbSet<IdentityUserToken<Guid>> UserTokens { get; }

        DbSet<TelegramUser> TelegramUsers { get; }
        DbSet<TelegramAuthCode> TelegramAuthCodes { get; }
        DbSet<TelegramMessageLog> TelegramMessageLogs { get; }
        DbSet<TelegramBotConfiguration> TelegramBotConfigurations { get; }
        DbSet<TelegramSession> TelegramSessions { get; }
        DbSet<TelegramSessionMessage> TelegramSessionMessages { get; }

        void ClearTracking();
        Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}
