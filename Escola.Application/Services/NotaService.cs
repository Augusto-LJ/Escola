namespace Escola.Application.Services;

using Escola.Application.DTOs.Nota;
using Escola.Application.Interfaces;
using Escola.Domain.Entities;
using Escola.Domain.Interfaces;

public class NotaService(INotaRepository notaRepository) : INotaService
{
    private readonly INotaRepository _notaRepository = notaRepository;

    public async Task<NotaGetDTO> AddAsync(NotaPostDTO nota)
    {
        var novaNota = new Nota
        {
            MatriculaId = nota.MatriculaId,
            ValorNota = nota.ValorNota,
            Aprovado = nota.ValorNota >= 60,
            Ativo = true
        };

        var notaCriada = await _notaRepository.AddAsync(novaNota);

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
        var notaDeletada = await _notaRepository.DeleteAsync(id);

        if (notaDeletada == null)
            return null;

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
        var nota = await _notaRepository.GetByIdAsync(id);

        if (nota == null)
            return null;

        return new NotaGetDTO
        {
            Id = nota.Id,
            MatriculaId = nota.MatriculaId,
            ValorNota = nota.ValorNota,
            Aprovado = nota.Aprovado,
            DataNota = nota.DataNota
        };
    }

    public async Task<NotaGetDTO> UpdateAsync(NotaPutDTO nota)
    {
        var notaParaAtualizar = new Nota
        {
            Id = nota.Id,
            MatriculaId = nota.MatriculaId,
            ValorNota = nota.ValorNota,
            Aprovado = nota.ValorNota >= 60,
            Ativo = true
        };

        var notaAtualizada = await _notaRepository.UpdateAsync(notaParaAtualizar);

        if (notaAtualizada == null)
            return null;

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
