using Escola.Application.DTOs.Matricula;
using Escola.Domain.Entities;
using Escola.Domain.Pagination;

namespace Escola.Application.Interfaces;

public interface IMatriculaService
{
    Task<MatriculaGetDetailDTO> GetByIdAsync(int id);
    Task<PagedList<MatriculaGetDetailDTO>> GetAllAsync(int pageNumber, int pageSize);
    Task<MatriculaGetDTO> AddAsync(MatriculaPostDTO matricula);
    Task<MatriculaGetDTO> UpdateAsync(MatriculaPutDTO matricula);
    Task<MatriculaGetDTO> DeleteAsync(int id);
}
