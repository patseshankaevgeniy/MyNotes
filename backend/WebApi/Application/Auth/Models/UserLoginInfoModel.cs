namespace Application.Auth.Models;

public sealed class UserLoginInfoModel
{
    public string LoginProvider { get; set; } = default!;
    public string ProviderKey { get; set; } = default!;
    public string ProviderDisplayName { get; set; } = default!;
}