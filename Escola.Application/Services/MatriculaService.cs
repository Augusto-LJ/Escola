using Escola.Application.DTOs.Matricula;
using Escola.Application.DTOs.Turma;
using Escola.Application.DTOs.Usuario;
using Escola.Application.Exceptions;
using Escola.Application.Interfaces;
using Escola.Domain.Entities;
using Escola.Domain.Interfaces;
using Escola.Domain.Pagination;

namespace Escola.Application.Services;

public class MatriculaService(IMatriculaRepository matriculaRepository, ITurmaRepository turmaRepository) : IMatriculaService
{
    private readonly IMatriculaRepository _matriculaRepository = matriculaRepository;
    private readonly ITurmaRepository _turmaRepository = turmaRepository;

    public async Task<MatriculaGetDTO> AddAsync(MatriculaPostDTO matricula)
    {
        var novaMatricula = new Matricula
        {
            UsuarioId = matricula.UsuarioId,
            TurmaId = matricula.TurmaId,
            DataMatricula = DateTime.UtcNow,
            DataExpiracao = matricula.DataExpiracao,
            Ativo = true
        };

        var matriculaCriada = await _matriculaRepository.AddAsync(novaMatricula) ?? throw new BadRequestException("Não foi possível criar a matrícula");

        return new MatriculaGetDTO
        {
            Id = matriculaCriada.Id,
            UsuarioId = matriculaCriada.UsuarioId,
            TurmaId = matriculaCriada.TurmaId,
            DataMatricula = matriculaCriada.DataMatricula,
            DataExpiracao = matriculaCriada.DataExpiracao,
            Ativo = matriculaCriada.Ativo
        };
    }

    public async Task<MatriculaGetDTO> DeleteAsync(int id)
    {
        var matriculaDeletada = await _matriculaRepository.DeleteAsync(id) ?? throw new NotFoundException("Matrícula não encontrada");

        return new MatriculaGetDTO
        {
            Id = matriculaDeletada.Id,
            UsuarioId = matriculaDeletada.UsuarioId,
            TurmaId = matriculaDeletada.TurmaId,
            DataMatricula = matriculaDeletada.DataMatricula,
            DataExpiracao = matriculaDeletada.DataExpiracao,
            Ativo = matriculaDeletada.Ativo
        };
    }

    public async Task<PagedList<MatriculaGetDetailDTO>> GetAllAsync(int pageNumber, int pageSize)
    {
        var todasMatriculas = await _matriculaRepository.GetAllAsync(pageNumber, pageSize);

        if (todasMatriculas == null || !todasMatriculas.Any())
            throw new NotFoundException("Nenhuma matrícula encontrada");

        var matriculasDTO = new List<MatriculaGetDetailDTO>();

        matriculasDTO.AddRange(todasMatriculas.Select(m => new MatriculaGetDetailDTO
        {
            Id = m.Id,
            Usuario = new UsuarioGetDTO
            {
                Id = m.Usuario.Id,
                Nome = m.Usuario.Nome,
                Email = m.Usuario.Email
            },
            Turma = new TurmaGetDTO
            {
                Id = m.Turma.Id,
                Nome = m.Turma.Nome,
                Descricao = m.Turma.Descricao
            },
            DataMatricula = m.DataMatricula,
            DataExpiracao = m.DataExpiracao,
            Ativo = m.Ativo
        }));

        return new PagedList<MatriculaGetDetailDTO>(matriculasDTO, todasMatriculas.CurrentPage, todasMatriculas.PageSize, todasMatriculas.TotalCount);
    }

    public async Task<MatriculaGetDetailDTO> GetByIdAsync(int id)
    {
        var matricula = await _matriculaRepository.GetByIdAsync(id) ?? throw new NotFoundException("Matrícula não encontrada");

        return new MatriculaGetDetailDTO
        {
            Id = matricula.Id,
            Usuario = new UsuarioGetDTO
            {
                Id = matricula.Usuario.Id,
                Nome = matricula.Usuario.Nome,
                Email = matricula.Usuario.Email
            },
            Turma = new TurmaGetDTO
            {
                Id = matricula.Turma.Id,
                Nome = matricula.Turma.Nome,
                Descricao = matricula.Turma.Descricao
            },
            DataMatricula = matricula.DataMatricula,
            DataExpiracao = matricula.DataExpiracao,
            Ativo = matricula.Ativo
        };
    }

    public async Task<MatriculaGetDTO> UpdateAsync(MatriculaPutDTO matricula)
    {
        var matriculaExistente = await _matriculaRepository.GetByIdAsync(matricula.Id) ?? throw new NotFoundException("Matrícula não encontrada");
        var turmaExistente = await _turmaRepository.GetByIdAsync(matricula.TurmaId) ?? throw new NotFoundException("Turma não encontrada");

        matriculaExistente.Id = matricula.Id;
        matriculaExistente.TurmaId = turmaExistente.Id;
        matriculaExistente.DataExpiracao = matricula.DataExpiracao;

        var matriculaAtualizada = await _matriculaRepository.UpdateAsync(matriculaExistente) ?? throw new BadRequestException("Não foi possível atualizar a matrícula");

        return new MatriculaGetDTO
        {
            Id = matriculaAtualizada.Id,
            UsuarioId = matriculaAtualizada.UsuarioId,
            TurmaId = matriculaAtualizada.TurmaId,
            DataMatricula = matriculaAtualizada.DataMatricula,
            DataExpiracao = matriculaAtualizada.DataExpiracao,
            Ativo = matriculaAtualizada.Ativo
        };
    }
}
