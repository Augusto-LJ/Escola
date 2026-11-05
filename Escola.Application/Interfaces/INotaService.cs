using Escola.Application.DTOs.Nota;
using Escola.Domain.Entities;

namespace Escola.Application.Interfaces;

public interface INotaService
{
    Task<NotaGetDTO> GetByIdAsync(int id);
    Task<List<NotaGetDTO>> GetAllAsync();
    Task<NotaGetDTO> AddAsync(NotaPostDTO nota);
    Task<NotaGetDTO> UpdateAsync(NotaPutDTO nota);
    Task<NotaGetDTO> DeleteAsync(int id);
    Task<List<NotaGetDTO>> GetNotasByTurmaUsuarioId(int turmaId, int usuarioId);
}
