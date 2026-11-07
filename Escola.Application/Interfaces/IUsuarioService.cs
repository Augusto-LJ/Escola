using Escola.Application.DTOs.Usuario;
using Escola.Domain.Pagination;

namespace Escola.Application.Interfaces;

public interface IUsuarioService
{
    Task<UsuarioGetDTO> GetByIdAsync(int id);
    Task<PagedList<UsuarioGetDTO>> GetAllAsync(int pageNumber, int pageSize);
    Task<UsuarioGetDTO> AddAsync(UsuarioPostDTO usuario);
    Task<UsuarioGetDTO> UpdateAsync(int usuarioId, UsuarioPutDTO usuario);
    Task<UsuarioGetDTO> DeleteAsync(int id);
    Task<bool> ExisteUsuarioAsync();
}
