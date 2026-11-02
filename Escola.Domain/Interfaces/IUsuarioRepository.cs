namespace Escola.Domain.Interfaces;

using Escola.Domain.Entities;

public interface IUsuarioRepository
{
    Task<Usuario> GetByIdAsync(int id);
    Task<List<Usuario>> GetAllAsync();
    Task<Usuario> AddAsync(Usuario usuario);
    Task<Usuario> UpdateAsync(Usuario usuario);
    Task<Usuario> DeleteAsync(int id);
    Task<bool> ExisteUsuarioAsync();
}
