using System.ComponentModel.DataAnnotations;

namespace Escola.API.Models;

public class PaginationParams
{
    [Range(1, int.MaxValue, ErrorMessage = "O número da página deve ser maior que zero.")]
    public int PageNumber { get; set; } = 1;

    [Range(1, 50, ErrorMessage = "O tamanho da página deve ser maior que zero e menor que cinquenta.")]
    public int PageSize { get; set; } = 10;
}
