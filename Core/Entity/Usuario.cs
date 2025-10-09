using System.ComponentModel.DataAnnotations;

namespace Core.Entity;

public class Usuario : EntityBase
{    
    public string Nome { get; set; }
    
    [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
    ErrorMessage = "A senha deve ter no mínimo 8 caracteres, incluindo letra maiúscula, minúscula, número e caractere especial.")]
    public string Senha { get; set; }

    public bool IsAdmin { get; set; }

    public Usuario() { }
    public Usuario(string name, string email, string password, string role, DateTime dataCriacao)
    {
        Nome = name;
        Email = email;
        Senha = password;        
        IsAdmin = string.Equals(role, "admin", StringComparison.OrdinalIgnoreCase);
        DataCriacao = dataCriacao;
    }

    public void UsuarioUpdate(string name, string email, string password, string role)
    {
        Nome = name;
        Email = email;
        Senha = password;
        IsAdmin = string.Equals(role, "admin", StringComparison.OrdinalIgnoreCase);
    }
}