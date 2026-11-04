using Escola.Application.DTOs.Curso;
using Escola.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Escola.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrador")]
public class CursoController(ICursoService cursoService) : Controller
{
    private readonly ICursoService _cursoService = cursoService;

    [HttpPost]
    public async Task<ActionResult> CreateCurso(CursoPostDTO curso)
    {
        var cursoCriado = await _cursoService.AddAsync(curso);
        return Ok(new { message = "Curso criado com sucesso" });
    }

    [HttpPut]
    public async Task<ActionResult> UpdateCurso(CursoPutDTO curso)
    {
        var cursoAtualizado = await _cursoService.UpdateAsync(curso);
        return Ok(new { message = "Curso atualizado com sucesso" });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCurso(int id)
    {
        var cursoDeletado = await _cursoService.DeleteAsync(id);
        return Ok(new { message = "Curso deletado com sucesso" });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetCursoById(int id)
    {
        var curso = await _cursoService.GetByIdAsync(id);
        return Ok(curso);
    }

    [HttpGet]
    public async Task<ActionResult> GetAllCursos()
    {
        var cursos = await _cursoService.GetAllAsync();
        return Ok(cursos);
    }
}
