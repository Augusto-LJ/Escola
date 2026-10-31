namespace Escola.Application.Services;

using Escola.Application.DTOs.Curso;
using Escola.Application.DTOs.Turma;
using Escola.Application.Exceptions;
using Escola.Application.Interfaces;
using Escola.Domain.Entities;
using Escola.Domain.Interfaces;

public class TurmaService(ITurmaRepository turmaRepository, ICursoRepository cursoRepository) : ITurmaService
{
    private readonly ITurmaRepository _turmaRepository = turmaRepository;
    private readonly ICursoRepository _cursoRepository = cursoRepository;

    public async Task<TurmaGetDTO> AddAsync(TurmaPostDTO turma)
    {
        var curso = await _cursoRepository.GetByIdAsync(turma.CursoId) ?? throw new NotFoundException("Curso não encontrado.");

        var novaTurma = new Turma
        {
            Nome = turma.Nome,
            Descricao = turma.Descricao,
            CursoId = curso.Id,
            Ativo = true
        };

        var turmaCriada = await _turmaRepository.AddAsync(novaTurma) ?? throw new BadRequestException("Não foi possível criar a turma");

        return new TurmaGetDTO
        {
            Id = turmaCriada.Id,
            Nome = turmaCriada.Nome,
            Descricao = turmaCriada.Descricao,
            CursoId = curso.Id
        };
    }

    public async Task<TurmaGetDTO> DeleteAsync(int id)
    {
        var turmaDeletada = await _turmaRepository.DeleteAsync(id) ?? throw new NotFoundException("Turma não encontrada.");

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
            throw new NotFoundException("Turma não encontrada.");

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
        var turmaExistente = await _turmaRepository.GetByIdAsync(turma.Id) ?? throw new NotFoundException("Turma não encontrada.");
        var cursoExistente = await _cursoRepository.GetByIdAsync(turma.CursoId) ?? throw new NotFoundException("Curso não encontrado.");

        turmaExistente.Id = turma.Id;
        turmaExistente.Nome = turma.Nome;
        turmaExistente.Descricao = turma.Descricao;
        turmaExistente.CursoId = cursoExistente.Id;

        var turmaAtualizada = await _turmaRepository.UpdateAsync(turmaExistente) ?? throw new BadRequestException("Não foi possível atualizar a turma");

        return new TurmaGetDTO
        {
            Id = turmaAtualizada.Id,
            Nome = turmaAtualizada.Nome,
            Descricao = turmaAtualizada.Descricao,
            CursoId = cursoExistente.Id
        };
    }
}
