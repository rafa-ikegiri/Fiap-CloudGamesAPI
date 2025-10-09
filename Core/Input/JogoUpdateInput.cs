namespace Core.Input;

public class JogoUpdateInput
{
    public int Id { get; set; }
    public required string Titulo { get; set; }
    public required string Genero { get; set; }
    public required string Plataforma { get; set; }
    public DateTime DtLancamento { get; set; }
    public required bool Multiplayer { get; set; }
}
