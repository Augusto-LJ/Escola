using Escola.Application.DTOs.Matricula;
using Escola.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Escola.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatriculaController(IMatriculaService matriculaService) : Controller
{
    private readonly IMatriculaService _matriculaService = matriculaService;
    [HttpPost]
    public async Task<ActionResult> CreateMatricula(MatriculaPostDTO matricula)
    {
        var matriculaCriada = await _matriculaService.AddAsync(matricula);

        if (matriculaCriada == null)
            return BadRequest("Não foi possível criar a matrícula");

        return Ok(new { message = "Matrícula criada com sucesso" });
    }

    [HttpPut]
    public async Task<ActionResult> UpdateMatricula(MatriculaPutDTO matricula)
    {
        var matriculaAtualizada = await _matriculaService.UpdateAsync(matricula);

        if (matriculaAtualizada == null)
            return BadRequest("Não foi possível atualizar a matrícula");

        return Ok(new { message = "Matrícula atualizada com sucesso" });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMatricula(int id)
    {
        var matriculaDeletada = await _matriculaService.DeleteAsync(id);

        if (matriculaDeletada == null)
            return BadRequest("Não foi possível deletar a matrícula");

        return Ok(new { message = "Matrícula deletada com sucesso" });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetMatriculaById(int id)
    {
        var matricula = await _matriculaService.GetByIdAsync(id);

        if (matricula == null)
            return NotFound("Matrícula não encontrada");

        return Ok(matricula);
    }

    [HttpGet]
    public async Task<ActionResult> GetAllMatriculas()
    {
        var matriculas = await _matriculaService.GetAllAsync();

        if (matriculas == null || matriculas.Count == 0)
            return NotFound("Nenhuma matrícula encontrada");

        return Ok(matriculas);
    }
}
