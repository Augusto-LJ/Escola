using Escola.Application.DTOs.Curso;
using Escola.Application.Exceptions;
using Escola.Application.Interfaces;
using Escola.Domain.Entities;
using Escola.Domain.Interfaces;
using Escola.Domain.Pagination;

namespace Escola.Application.Services;

public class CursoService(ICursoRepository cursoRepository) : ICursoService
{
    private readonly ICursoRepository _cursoRepository = cursoRepository;

    public async Task<CursoGetDTO> AddAsync(CursoPostDTO curso)
    {
        var novoCurso = new Curso
        {
            Nome = curso.Nome,
            Descricao = curso.Descricao,
        };

        var cursoAdicionado = await _cursoRepository.AddAsync(novoCurso) ?? throw new BadRequestException("Não foi possível criar o curso");

        return new CursoGetDTO
        {
            Id = cursoAdicionado.Id,
            Nome = cursoAdicionado.Nome,
            Descricao = cursoAdicionado.Descricao
        };
    }

    public async Task<CursoGetDTO> DeleteAsync(int id)
    {
        var cursoDeletado = await _cursoRepository.DeleteAsync(id) ?? throw new NotFoundException("Curso não encontrado.");

        return new CursoGetDTO
        {
            Id = cursoDeletado.Id,
            Nome = cursoDeletado.Nome,
            Descricao = cursoDeletado.Descricao
        };
    }

    public async Task<PagedList<CursoGetDTO>> GetAllAsync(int pageNumber, int pageSize)
    {
        var todosCursos = await _cursoRepository.GetAllAsync(pageNumber, pageSize);
        var cursosDTO = new List<CursoGetDTO>();

        cursosDTO.AddRange(todosCursos.Select(curso => new CursoGetDTO
        {
            Id = curso.Id,
            Nome = curso.Nome,
            Descricao = curso.Descricao
        }));

        return new PagedList<CursoGetDTO>(cursosDTO, todosCursos.TotalCount, todosCursos.CurrentPage, todosCursos.PageSize);
    }

    public async Task<CursoGetDTO> GetByIdAsync(int id)
    {
        var curso = await _cursoRepository.GetByIdAsync(id) ?? throw new NotFoundException("Curso não encontrado.");

        return new CursoGetDTO
        {
            Id = curso.Id,
            Nome = curso.Nome,
            Descricao = curso.Descricao
        };
    }

    public async Task<CursoGetDTO> UpdateAsync(CursoPutDTO curso)
    {
        var cursoExistente = await _cursoRepository.GetByIdAsync(curso.Id) ?? throw new NotFoundException("Curso não encontrado");

        cursoExistente.Id = curso.Id;
        cursoExistente.Nome = curso.Nome;
        cursoExistente.Descricao = curso.Descricao;

        var cursoAtualizado = await _cursoRepository.UpdateAsync(cursoExistente) ?? throw new BadRequestException("Não foi possível atualizar o curso");

        return new CursoGetDTO
        {
            Id = cursoAtualizado.Id,
            Nome = cursoAtualizado.Nome,
            Descricao = cursoAtualizado.Descricao
        };
    }
}
