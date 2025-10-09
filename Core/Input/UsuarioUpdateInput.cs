﻿namespace Core.Input;

public class UsuarioUpdateInput
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public required string Email { get; set; }
    public required string Senha { get; set; }
    public bool IsAdmin { get; set; }
}
