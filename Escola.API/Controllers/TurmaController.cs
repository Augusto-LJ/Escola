using Escola.Application.DTOs.Turma;
using Escola.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Escola.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TurmaController(ITurmaService turmaService) : Controller
{
    private readonly ITurmaService _turmaService = turmaService;

    [HttpPost]
    public async Task<ActionResult> CreateTurma(TurmaPostDTO turma)
    {
        var turmaCriada = await _turmaService.AddAsync(turma);
        return Ok(new { message = "Turma criada com sucesso" });
    }

    [HttpPut]
    public async Task<ActionResult> UpdateTurma(TurmaPutDTO turma)
    {
        var turmaAtualizada = await _turmaService.UpdateAsync(turma);
        return Ok(new { message = "Turma atualizada com sucesso" });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTurma(int id)
    {
        var turmaDeletada = await _turmaService.DeleteAsync(id);
        return Ok(new { message = "Turma deletada com sucesso" });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetTurmaById(int id)
    {
        var turma = await _turmaService.GetByIdAsync(id);
        return Ok(turma);
    }

    [HttpGet]
    public async Task<ActionResult> GetAllTurmas()
    {
        var turmas = await _turmaService.GetAllAsync();
        return Ok(turmas);
    }
}
