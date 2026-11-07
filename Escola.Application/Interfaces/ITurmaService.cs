using Escola.Application.DTOs.Turma;
using Escola.Domain.Pagination;

namespace Escola.Application.Interfaces;

public interface ITurmaService
{
    Task<TurmaGetDetailDTO> GetByIdAsync(int id);
    Task<PagedList<TurmaGetDetailDTO>> GetAllAsync(int pageNumber, int pageSize);
    Task<TurmaGetDTO> AddAsync(TurmaPostDTO turma);
    Task<TurmaGetDTO> UpdateAsync(TurmaPutDTO turma);
    Task<TurmaGetDTO> DeleteAsync(int id);
    Task<PagedList<TurmaGetDetailDTO>> GetTurmasByUsuarioId(int usuarioId, int pageNumber, int pageSize);
}
