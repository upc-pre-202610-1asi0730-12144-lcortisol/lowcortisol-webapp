namespace LowCortisol.Application.Contexts.IdentityAccess.Commands;

public class LoginUserCommand
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}