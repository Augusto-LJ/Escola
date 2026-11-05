using Escola.Application.DTOs.Nota;
using Escola.Application.Interfaces;
using Escola.Infra.IoC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Escola.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotaController(INotaService notaService) : Controller
{
    private readonly INotaService _notaService = notaService;

    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult> CreateNota(NotaPostDTO nota)
    {
        var notaCriada = await _notaService.AddAsync(nota);
        return Ok(new { message = "Nota criada com sucesso" });
    }

    [HttpPut]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult> UpdateNota(NotaPutDTO nota)
    {
        var notaAtualizada = await _notaService.UpdateAsync(nota);
        return Ok(new { message = "Nota atualizada com sucesso" });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult> DeleteNota(int id)
    {
        var notaDeletada = await _notaService.DeleteAsync(id);
        return Ok(new { message = "Nota deletada com sucesso" });
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult> GetNotaById(int id)
    {
        var nota = await _notaService.GetByIdAsync(id);
        return Ok(nota);
    }

    [HttpGet]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult> GetAllNotas()
    {
        var notas = await _notaService.GetAllAsync();
        return Ok(notas);
    }

    [HttpGet("usuario/turma/{turmaId}")]
    [Authorize(Roles = "Aluno,Administrador")]
    public async Task<ActionResult> GetNotasByTurmaUsuarioId(int turmaId)
    {
        var usuarioId = User.GetUsuarioId();
        var notas = await _notaService.GetNotasByTurmaUsuarioId(turmaId, usuarioId);
        return Ok(notas);
    }
}
