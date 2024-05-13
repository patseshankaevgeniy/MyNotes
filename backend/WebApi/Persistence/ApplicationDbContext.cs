using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Entities.NewFolder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Infrastructure;
using System;

namespace Persistence;

public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>, IApplicationDbContext
{
    public DbSet<Group> Groups => Set<Group>();
    public DbSet<Language> Languages => Set<Language>();
    public DbSet<UserNote> UserNotes => Set<UserNote>();
    public DbSet<IdentityUserToken<Guid>> UserTokens => Set<IdentityUserToken<Guid>>();
    public DbSet<MembershipStatus> MembershipStatuses => Set<MembershipStatus>();

    public DbSet<TelegramUser> TelegramUsers => Set<TelegramUser>();
    public DbSet<TelegramAuthCode> TelegramAuthCodes => Set<TelegramAuthCode>();
    public DbSet<TelegramMessageLog> TelegramMessageLogs => Set<TelegramMessageLog>();
    public DbSet<TelegramBotConfiguration> TelegramBotConfigurations => Set<TelegramBotConfiguration>();
    public DbSet<TelegramSession> TelegramSessions => Set<TelegramSession>();
    public DbSet<TelegramSessionMessage> TelegramSessionMessages => Set<TelegramSessionMessage>();


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    public void ClearTracking()
    {
        ChangeTracker.Clear();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        builder.ApplySeedData();
    }
}
