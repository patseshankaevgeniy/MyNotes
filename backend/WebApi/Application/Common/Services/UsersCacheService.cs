using Application.Common.Interfaces;
using Application.Users.Models;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Services;

public sealed class UsersCacheService : IUsersCacheService
{
    private const string UserWithMembersKey = "user-with-members-{0}";
    private static readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);

    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _db;
    private readonly IMemoryCache _memoryCache;

    public UsersCacheService(
        IMapper mapper,
        IApplicationDbContext db,
        IMemoryCache memoryCache)
    {
        _db = db;
        _mapper = mapper;
        _memoryCache = memoryCache;
    }

    public async Task<UserWithMembersModel> GetCurrentUserWithMembersAsync(Guid userId)
    {
        await _semaphoreSlim.WaitAsync();

        try
        {
            var key = string.Format(UserWithMembersKey, userId);

            if (!_memoryCache.TryGetValue<UserWithMembersModel>(key, out var userWithMembers))
            {
                await InitializeAsync();
                userWithMembers = _memoryCache.Get<UserWithMembersModel>(string.Format(UserWithMembersKey, userId));
            }

            return userWithMembers;
        }
        finally
        {
            _semaphoreSlim.Release();
        }
    }

    public async Task ResetCacheAsync()
    {
        await _semaphoreSlim.WaitAsync();

        try
        {
            await InitializeAsync();
        }
        finally
        {
            _semaphoreSlim.Release();
        }
    }

    private async Task InitializeAsync()
    {
        var users = await _db.Users
                 .Include(u => u.MembershipStatus)
                 .Include(u => u.Group).ThenInclude(g => g!.Users)
                 .ToListAsync();

        foreach (var user in users)
        {
            var userGroupMembersIds = new List<Guid>();

            if (user.Group != null && user.MembershipStatus != null)
            {
                var acceptedUsersIds = user.Group.Users
                    .Where(u => u.MembershipStatus?.Status == AcceptanceStatus.Completed)
                    .Select(u => u.Id)
                    .ToList();

                if (acceptedUsersIds.Count > 1 && acceptedUsersIds.Any(userId => userId == user.Id))
                {
                    userGroupMembersIds.AddRange(acceptedUsersIds);
                }
            }

            var userWithMembers = new UserWithMembersModel
            {
                UserModel = _mapper.Map<UserModel>(user),
                UserGroupMembersIds = userGroupMembersIds
            };

            _memoryCache.Remove(string.Format(UserWithMembersKey, user.Id));
            _memoryCache.Set(
                string.Format(UserWithMembersKey, user.Id),
                userWithMembers,
                TimeSpan.FromDays(1));
        }
    }
}

public sealed class UserWithMembersModel
{
    public UserModel UserModel { get; init; } = default!;
    public IEnumerable<Guid> UserGroupMembersIds { get; init; } = new List<Guid>();
}