using Escola.Application.DTOs.Curso;
using Escola.Application.Interfaces;
using Escola.Domain.Entities;
using Escola.Domain.Interfaces;

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

        var cursoAdicionado = await _cursoRepository.AddAsync(novoCurso);

        return new CursoGetDTO
        {
            Id = cursoAdicionado.Id,
            Nome = cursoAdicionado.Nome,
            Descricao = cursoAdicionado.Descricao
        };
    }

    public async Task<CursoGetDTO> DeleteAsync(int id)
    {
        var cursoDeletado = await _cursoRepository.DeleteAsync(id);

        if (cursoDeletado == null)
            return null;

        return new CursoGetDTO
        {
            Id = cursoDeletado.Id,
            Nome = cursoDeletado.Nome,
            Descricao = cursoDeletado.Descricao
        };
    }

    public async Task<List<CursoGetDTO>> GetAllAsync()
    {
        var todosCursos = await _cursoRepository.GetAllAsync();
        var cursosDTO = new List<CursoGetDTO>();

        cursosDTO.AddRange(todosCursos.Select(curso => new CursoGetDTO
        {
            Id = curso.Id,
            Nome = curso.Nome,
            Descricao = curso.Descricao
        }));

        return cursosDTO;
    }

    public async Task<CursoGetDTO> GetByIdAsync(int id)
    {
        var curso = await _cursoRepository.GetByIdAsync(id);

        if (curso == null)
            return null;

        return new CursoGetDTO
        {
            Id = curso.Id,
            Nome = curso.Nome,
            Descricao = curso.Descricao
        };
    }

    public async Task<CursoGetDTO> UpdateAsync(CursoPutDTO curso)
    {
        var cursoParaAtualizar = new Curso
        {
            Id = curso.Id,
            Nome = curso.Nome,
            Descricao = curso.Descricao
        };

        var cursoAtualizado = await _cursoRepository.UpdateAsync(cursoParaAtualizar);

        if (cursoAtualizado == null)
            return null;

        return new CursoGetDTO
        {
            Id = cursoAtualizado.Id,
            Nome = cursoAtualizado.Nome,
            Descricao = cursoAtualizado.Descricao
        };
    }
}
