using System.ComponentModel.DataAnnotations;

namespace Escola.API.Models;

public class UserLogin
{
    [Required(ErrorMessage = "O email é obrigatório.")]
    [MaxLength(200, ErrorMessage = "O email não pode ter mais de 200 caracteres.")]
    [EmailAddress(ErrorMessage = "O email não é válido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória.")]
    [MinLength(8, ErrorMessage = "A senha deve ter no mínimo 8 caracteres.")]
    [MaxLength(100, ErrorMessage = "A senha não pode ter mais de 100 caracteres.")]
    public string Senha { get; set; }
}
