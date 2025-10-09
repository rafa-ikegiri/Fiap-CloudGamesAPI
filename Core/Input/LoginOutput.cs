namespace Core.Input;

public class LoginOutput
{
    public required string Token { get; set; }
    public required string Nome { get; set; }
    public required string Email { get; set; }

    public bool IsAdmin { get; set; }
}
