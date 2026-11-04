using Escola.Application.DTOs.Turma;

namespace Escola.Application.Interfaces;

public interface ITurmaService
{
    Task<TurmaGetDetailDTO> GetByIdAsync(int id);
    Task<List<TurmaGetDetailDTO>> GetAllAsync();
    Task<TurmaGetDTO> AddAsync(TurmaPostDTO turma);
    Task<TurmaGetDTO> UpdateAsync(TurmaPutDTO turma);
    Task<TurmaGetDTO> DeleteAsync(int id);
    Task<List<TurmaGetDetailDTO>> GetTurmasByUsuarioId(int usuarioId);
}
