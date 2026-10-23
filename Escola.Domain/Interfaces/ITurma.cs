namespace Escola.Domain.Interfaces;

using Escola.Domain.Entities;

public interface ITurma
{
    Task<Turma> GetByIdAsync(int id);
    Task<List<Turma>> GetAllAsync();
    Task<Turma> AddAsync(Turma turma);
    Task<Turma> UpdateAsync(Turma turma);
    Task<Turma> DeleteAsync(int id);
}
