namespace Escola.Domain.Interfaces;

using Escola.Domain.Entities;
using Escola.Domain.Pagination;

public interface INotaRepository
{
    Task<Nota> GetByIdAsync(int id);
    Task<PagedList<Nota>> GetAllAsync(int pageNumber, int pageSize);
    Task<Nota> AddAsync(Nota nota);
    Task<Nota> UpdateAsync(Nota nota);
    Task<Nota> DeleteAsync(int id);
    Task<PagedList<Nota>> GetNotasByTurmaUsuarioId(int turmaId, int usuarioId, int pageNumber, int pageSize);
}
