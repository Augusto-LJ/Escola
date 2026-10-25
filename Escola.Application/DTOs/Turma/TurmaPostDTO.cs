using System.ComponentModel.DataAnnotations;

namespace Escola.Application.DTOs.Turma;

public class TurmaPostDTO
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MaxLength(50, ErrorMessage = "O nome não pode ter mais de 50 caracteres.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "A descrição é obrigatória.")]
    [MaxLength(150, ErrorMessage = "A descrição não pode ter mais de 150 caracteres.")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "O curso é obrigatório.")]
    public int CursoId { get; set; }
}
