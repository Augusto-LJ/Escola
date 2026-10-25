using Escola.Application.DTOs.Matricula;
using Escola.Domain.Entities;

namespace Escola.Application.Interfaces;

public interface IMatriculaService
{
    Task<MatriculaGetDetailDTO> GetByIdAsync(int id);
    Task<List<MatriculaGetDetailDTO>> GetAllAsync();
    Task<MatriculaGetDTO> AddAsync(MatriculaPostDTO matricula);
    Task<MatriculaGetDTO> UpdateAsync(MatriculaPutDTO matricula);
    Task<MatriculaGetDTO> DeleteAsync(int id);
}
