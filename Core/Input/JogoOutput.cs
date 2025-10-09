namespace Core.Input;

public class JogoOutput
{
    public required string Titulo { get; set; }
    public required string Genero { get; set; }
    public required string Plataforma { get; set; }
    public DateTime DtLancamento { get; set; }
    public required bool Multiplayer { get; set; }
}
