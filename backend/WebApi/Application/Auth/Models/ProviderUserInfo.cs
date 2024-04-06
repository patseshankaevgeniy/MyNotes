namespace Application.Auth.Models;

public sealed class ProviderUserInfo
{
    public string UserName { get; set; }
    public string UserFirstName { get; set; }
    public string UserLastName { get; set; }
    public string UserEmail { get; set; } = default!;
    public string UserImageUrl { get; set; } = default!;

    public UserLoginInfoModel UserLoginInfoModel { get; set; } = default!;
}