namespace Escola.Application.Services;

using Escola.Application.DTOs.Nota;
using Escola.Application.Exceptions;
using Escola.Application.Interfaces;
using Escola.Domain.Entities;
using Escola.Domain.Interfaces;

public class NotaService(INotaRepository notaRepository, IMatriculaRepository matriculaRepository) : INotaService
{
    private readonly INotaRepository _notaRepository = notaRepository;
    private readonly IMatriculaRepository _matriculaRepository = matriculaRepository;

    public async Task<NotaGetDTO> AddAsync(NotaPostDTO nota)
    {
        var matriculaExistente = await _matriculaRepository.GetByIdAsync(nota.MatriculaId) ?? throw new NotFoundException("Matrícula não encontrada");

        var novaNota = new Nota
        {
            MatriculaId = matriculaExistente.Id,
            ValorNota = nota.ValorNota,
            Aprovado = nota.ValorNota >= 60,
            Ativo = true
        };

        var notaCriada = await _notaRepository.AddAsync(novaNota) ?? throw new BadRequestException("Não foi possível criar a nota");

        return new NotaGetDTO
        {
            Id = notaCriada.Id,
            MatriculaId = notaCriada.MatriculaId,
            ValorNota = notaCriada.ValorNota,
            Aprovado = notaCriada.Aprovado,
            DataNota = notaCriada.DataNota
        };
    }

    public async Task<NotaGetDTO> DeleteAsync(int id)
    {
        var notaDeletada = await _notaRepository.DeleteAsync(id) ?? throw new NotFoundException("Não foi possível encontrar a nota.");

        return new NotaGetDTO
        {
            Id = notaDeletada.Id,
            MatriculaId = notaDeletada.MatriculaId,
            ValorNota = notaDeletada.ValorNota,
            Aprovado = notaDeletada.Aprovado,
            DataNota = notaDeletada.DataNota
        };
    }

    public async Task<List<NotaGetDTO>> GetAllAsync()
    {
        var todasNotas = await _notaRepository.GetAllAsync();

        var notasDTO = new List<NotaGetDTO>();

        notasDTO.AddRange(todasNotas.Select(n => new NotaGetDTO
        {
            Id = n.Id,
            MatriculaId = n.MatriculaId,
            ValorNota = n.ValorNota,
            Aprovado = n.Aprovado,
            DataNota = n.DataNota
        }));

        return notasDTO;
    }

    public async Task<NotaGetDTO> GetByIdAsync(int id)
    {
        var matriculaExistente = await _matriculaRepository.GetByIdAsync(id) ?? throw new NotFoundException("Matrícula não encontrada");
        var notaExistente = await _notaRepository.GetByIdAsync(id) ?? throw new NotFoundException("Nota não encontrada");

        return new NotaGetDTO
        {
            Id = notaExistente.Id,
            MatriculaId = matriculaExistente.Id,
            ValorNota = notaExistente.ValorNota,
            Aprovado = notaExistente.Aprovado,
            DataNota = notaExistente.DataNota
        };
    }

    public async Task<NotaGetDTO> UpdateAsync(NotaPutDTO nota)
    {
        var notaExistente = await _notaRepository.GetByIdAsync(nota.Id) ?? throw new NotFoundException("Nota não encontrada");
        var matriculaExistente = await _matriculaRepository.GetByIdAsync(nota.MatriculaId) ?? throw new NotFoundException("Matrícula não encontrada");

        notaExistente.Id = nota.Id;
        notaExistente.MatriculaId = matriculaExistente.Id;
        notaExistente.ValorNota = nota.ValorNota;
        notaExistente.Aprovado = nota.ValorNota >= 60;
        notaExistente.Ativo = true;

        var notaAtualizada = await _notaRepository.UpdateAsync(notaExistente) ?? throw new BadRequestException("Não foi possível atualizar a nota");

        return new NotaGetDTO
        {
            Id = notaAtualizada.Id,
            MatriculaId = notaAtualizada.MatriculaId,
            ValorNota = notaAtualizada.ValorNota,
            Aprovado = notaAtualizada.Aprovado,
            DataNota = notaAtualizada.DataNota
        };
    }
}
