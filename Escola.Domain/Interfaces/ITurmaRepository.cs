namespace Escola.Domain.Interfaces;

using Escola.Domain.Entities;
using Escola.Domain.Pagination;

public interface ITurmaRepository
{
    Task<Turma> GetByIdAsync(int id);
    Task<PagedList<Turma>> GetAllAsync(int pageNumber, int pageSize);
    Task<Turma> AddAsync(Turma turma);
    Task<Turma> UpdateAsync(Turma turma);
    Task<Turma> DeleteAsync(int id);
    Task<PagedList<Turma>> GetTurmasByUsuarioId(int usuarioId, int pageNumber, int pageSize);
}
