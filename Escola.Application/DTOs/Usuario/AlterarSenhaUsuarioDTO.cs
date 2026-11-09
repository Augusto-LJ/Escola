using System.ComponentModel.DataAnnotations;

namespace Escola.Application.DTOs.Usuario;

public class AlterarSenhaUsuarioDTO
{
    [Required(ErrorMessage = "A senha atual é obrigatória.")]
    [MaxLength(100, ErrorMessage = "A senha não pode ter mais de 100 caracteres.")]
    public string SenhaAtual { get; set; }

    [Required(ErrorMessage = "A nova senha é obrigatória.")]
    [MinLength(8, ErrorMessage = "A nova senha deve ter no mínimo 8 caracteres.")]
    [MaxLength(100, ErrorMessage = "A nova senha não pode ter mais de 100 caracteres.")]
    public string NovaSenha { get; set; }
}
