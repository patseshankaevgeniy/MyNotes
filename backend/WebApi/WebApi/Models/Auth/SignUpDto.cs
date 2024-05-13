namespace WebApi.Models.Auth;

public sealed class SignUpDto
{
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}