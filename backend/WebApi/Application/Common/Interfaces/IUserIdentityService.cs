using Application.Auth.Models;
using Application.Users.Models;
using System.Threading.Tasks;

namespace Application.Common.Interfaces;

public interface IUserIdentityService
{
    Task<UserModel?> FindByLoginAsync(UserLoginInfoModel loginInfoModel);
    Task LoginAsync(UserModel userModel, UserLoginInfoModel loginInfoModel);
}