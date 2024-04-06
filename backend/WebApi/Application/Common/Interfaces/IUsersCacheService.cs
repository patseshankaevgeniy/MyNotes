using Application.Common.Services;
using System;
using System.Threading.Tasks;

namespace Application.Common.Interfaces;

public interface IUsersCacheService
{
    Task ResetCacheAsync();
    Task<UserWithMembersModel> GetCurrentUserWithMembersAsync(Guid userId);
}
