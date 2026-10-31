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
        return Ok(new { message = "Matrícula criada com sucesso" });
    }

    [HttpPut]
    public async Task<ActionResult> UpdateMatricula(MatriculaPutDTO matricula)
    {
        var matriculaAtualizada = await _matriculaService.UpdateAsync(matricula);
        return Ok(new { message = "Matrícula atualizada com sucesso" });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMatricula(int id)
    {
        var matriculaDeletada = await _matriculaService.DeleteAsync(id);
        return Ok(new { message = "Matrícula deletada com sucesso" });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetMatriculaById(int id)
    {
        var matricula = await _matriculaService.GetByIdAsync(id);
        return Ok(matricula);
    }

    [HttpGet]
    public async Task<ActionResult> GetAllMatriculas()
    {
        var matriculas = await _matriculaService.GetAllAsync();
        return Ok(matriculas);
    }
}
