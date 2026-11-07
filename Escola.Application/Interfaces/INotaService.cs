using Escola.Application.DTOs.Nota;
using Escola.Domain.Entities;
using Escola.Domain.Pagination;

namespace Escola.Application.Interfaces;

public interface INotaService
{
    Task<NotaGetDTO> GetByIdAsync(int id);
    Task<PagedList<NotaGetDTO>> GetAllAsync(int pageNumber, int pageSize);
    Task<NotaGetDTO> AddAsync(NotaPostDTO nota);
    Task<NotaGetDTO> UpdateAsync(NotaPutDTO nota);
    Task<NotaGetDTO> DeleteAsync(int id);
    Task<PagedList<NotaGetDTO>> GetNotasByTurmaUsuarioId(int turmaId, int usuarioId, int pageNumber, int pageSize);
}
