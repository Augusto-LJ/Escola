using Escola.Application.DTOs.Usuario;

namespace Escola.Application.Interfaces;

public interface IUsuarioService
{
    Task<UsuarioGetDTO> GetByIdAsync(int id);
    Task<List<UsuarioGetDTO>> GetAllAsync();
    Task<UsuarioGetDTO> AddAsync(UsuarioPostDTO usuario);
    Task<UsuarioGetDTO> UpdateAsync(int usuarioId, UsuarioPutDTO usuario);
    Task<UsuarioGetDTO> DeleteAsync(int id);
    Task<bool> ExisteUsuarioAsync();
}
