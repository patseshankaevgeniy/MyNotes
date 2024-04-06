namespace WebApi.Models.Auth;

public sealed class SignUpDto
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string Password { get; set; }
}