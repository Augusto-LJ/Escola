namespace Escola.Domain.Interfaces;

using Escola.Domain.Entities;

public interface IMatricula
{
    Task<Matricula> GetByIdAsync(int id);
    Task<List<Matricula>> GetAllAsync();
    Task<Matricula> AddAsync(Matricula matricula);
    Task<Matricula> UpdateAsync(Matricula matricula);
    Task<Matricula> DeleteAsync(int id);
}
