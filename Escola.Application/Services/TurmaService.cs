namespace Escola.Application.Services;

using Escola.Application.DTOs.Curso;
using Escola.Application.DTOs.Turma;
using Escola.Application.Interfaces;
using Escola.Domain.Entities;
using Escola.Domain.Interfaces;

public class TurmaService(ITurmaRepository turmaRepository) : ITurmaService
{
    private readonly ITurmaRepository _turmaRepository = turmaRepository;

    public async Task<TurmaGetDTO> AddAsync(TurmaPostDTO turma)
    {
        var novaTurma = new Turma
        {
            Nome = turma.Nome,
            Descricao = turma.Descricao,
            CursoId = turma.CursoId,
            Ativo = true
        };

        var turmaCriada = await _turmaRepository.AddAsync(novaTurma);

        return new TurmaGetDTO
        {
            Id = turmaCriada.Id,
            Nome = turmaCriada.Nome,
            Descricao = turmaCriada.Descricao,
            CursoId = turmaCriada.CursoId
        };
    }

    public async Task<TurmaGetDTO> DeleteAsync(int id)
    {
        var turmaDeletada = await _turmaRepository.DeleteAsync(id);

        if (turmaDeletada == null)
            return null;

        return new TurmaGetDTO
        {
            Id = turmaDeletada.Id,
            Nome = turmaDeletada.Nome,
            Descricao = turmaDeletada.Descricao,
            CursoId = turmaDeletada.CursoId
        };
    }

    public async Task<List<TurmaGetDetailDTO>> GetAllAsync()
    {
        var todasTurmas = await _turmaRepository.GetAllAsync();

        var turmasDTO = new List<TurmaGetDetailDTO>();

        turmasDTO.AddRange(todasTurmas.Select(t => new TurmaGetDetailDTO
        {
            Id = t.Id,
            Nome = t.Nome,
            Descricao = t.Descricao,
            Curso = new CursoGetDTO
            {
                Id = t.Curso.Id,
                Nome = t.Curso.Nome,
                Descricao = t.Curso.Descricao
            }
        }));

        return turmasDTO;
    }

    public async Task<TurmaGetDetailDTO> GetByIdAsync(int id)
    {
        var turma = await _turmaRepository.GetByIdAsync(id);

        if (turma == null)
            return null;

        return new TurmaGetDetailDTO
        {
            Id = turma.Id,
            Nome = turma.Nome,
            Descricao = turma.Descricao,
            Curso = new CursoGetDTO
            {
                Id = turma.Curso.Id,
                Nome = turma.Curso.Nome,
                Descricao = turma.Curso.Descricao
            }
        };
    }

    public async Task<TurmaGetDTO> UpdateAsync(TurmaPutDTO turma)
    {
        var turmaParaAtualizar = new Turma
        {
            Id = turma.Id,
            Nome = turma.Nome,
            Descricao = turma.Descricao,
            CursoId = turma.CursoId
        };

        var turmaAtualizada = await _turmaRepository.UpdateAsync(turmaParaAtualizar);

        if (turmaAtualizada == null)
            return null;

        return new TurmaGetDTO
        {
            Id = turmaAtualizada.Id,
            Nome = turmaAtualizada.Nome,
            Descricao = turmaAtualizada.Descricao,
            CursoId = turmaAtualizada.CursoId
        };
    }
}
