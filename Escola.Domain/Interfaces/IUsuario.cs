namespace Escola.Domain.Interfaces;

using Escola.Domain.Entities;

public interface IUsuario
{
    Task<Usuario> GetByIdAsync(int id);
    Task<List<Usuario>> GetAllAsync();
    Task<Usuario> AddAsync(Usuario usuario);
    Task<Usuario> UpdateAsync(Usuario usuario);
    Task<Usuario> DeleteAsync(int id);
}
