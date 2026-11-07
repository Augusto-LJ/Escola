using Escola.API.Extensions;
using Escola.API.Models;
using Escola.Application.DTOs.Turma;
using Escola.Application.Interfaces;
using Escola.Infra.IoC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Escola.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TurmaController(ITurmaService turmaService) : Controller
{
    private readonly ITurmaService _turmaService = turmaService;

    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult> CreateTurma(TurmaPostDTO turma)
    {
        var turmaCriada = await _turmaService.AddAsync(turma);
        return Ok(new { message = "Turma criada com sucesso" });
    }

    [HttpPut]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult> UpdateTurma(TurmaPutDTO turma)
    {
        var turmaAtualizada = await _turmaService.UpdateAsync(turma);
        return Ok(new { message = "Turma atualizada com sucesso" });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult> DeleteTurma(int id)
    {
        var turmaDeletada = await _turmaService.DeleteAsync(id);
        return Ok(new { message = "Turma deletada com sucesso" });
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult> GetTurmaById(int id)
    {
        var turma = await _turmaService.GetByIdAsync(id);
        return Ok(turma);
    }

    [HttpGet]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult> GetAllTurmas([FromQuery] PaginationParams paginationParams)
    {
        var turmas = await _turmaService.GetAllAsync(paginationParams.PageNumber, paginationParams.PageSize);
        Response.AddPaginationHeader(new PaginationHeader(paginationParams.PageNumber, turmas.TotalPages, paginationParams.PageSize, turmas.TotalCount));
        return Ok(turmas);
    }

    [HttpGet("usuario")]
    [Authorize(Roles = "Aluno,Administrador")]
    public async Task<ActionResult> GetTurmasByUsuarioId([FromQuery] PaginationParams paginationParams)
    {
        var usuarioId = User.GetUsuarioId();
        var turmas = await _turmaService.GetTurmasByUsuarioId(usuarioId, paginationParams.PageNumber, paginationParams.PageSize);
        Response.AddPaginationHeader(new PaginationHeader(paginationParams.PageNumber, turmas.TotalPages, paginationParams.PageSize, turmas.TotalCount));
        return Ok(turmas);
    }
}
