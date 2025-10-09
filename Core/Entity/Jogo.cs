using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entity;

public class Jogo : EntityBase
{
    [Required(ErrorMessage = "O título do jogo é obrigatório")]
    [StringLength(20, ErrorMessage = "O título do jogo não pode exceder 20 caracteres")]
    public required string Titulo { get; set; }

    [Required(ErrorMessage = "O genêro do jogo é obrigatório")]
    public required string Genero { get; set; }

    [Required(ErrorMessage = "A plataforma do jogo é obrigatório")]
    public required string Plataforma { get; set; }
    public DateTime DtLancamento { get; set; }

    [Required(ErrorMessage = "Informar se o jogo é multiplayer")]
    public required bool Multiplayer { get; set; }
}