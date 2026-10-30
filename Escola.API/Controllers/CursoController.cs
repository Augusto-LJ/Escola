using Escola.Application.DTOs.Curso;
using Escola.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Escola.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CursoController(ICursoService cursoService) : Controller
{
    private readonly ICursoService _cursoService = cursoService;
    [HttpPost]
    public async Task<ActionResult> CreateCurso(CursoPostDTO curso)
    {
        var cursoCriado = await _cursoService.AddAsync(curso);

        if (cursoCriado == null)
            return BadRequest("Não foi possível criar o curso");

        return Ok(new { message = "Curso criado com sucesso" });
    }

    [HttpPut]
    public async Task<ActionResult> UpdateCurso(CursoPutDTO curso)
    {
        var cursoAtualizado = await _cursoService.UpdateAsync(curso);

        if (cursoAtualizado == null)
            return BadRequest("Não foi possível atualizar o curso");

        return Ok(new { message = "Curso atualizado com sucesso" });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCurso(int id)
    {
        var cursoDeletado = await _cursoService.DeleteAsync(id);

        if (cursoDeletado == null)
            return BadRequest("Não foi possível deletar o curso");

        return Ok(new { message = "Curso deletado com sucesso" });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetCursoById(int id)
    {
        var curso = await _cursoService.GetByIdAsync(id);

        if (curso == null)
            return NotFound("Curso não encontrado");

        return Ok(curso);
    }

    [HttpGet]
    public async Task<ActionResult> GetAllCursos()
    {
        var cursos = await _cursoService.GetAllAsync();

        if (cursos == null || cursos.Count == 0)
            return NotFound("Nenhum curso encontrado");

        return Ok(cursos);
    }
}
