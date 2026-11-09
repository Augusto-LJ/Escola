using Escola.API.Extensions;
using Escola.API.Models;
using Escola.Application.DTOs.Usuario;
using Escola.Application.Interfaces;
using Escola.Domain.Account;
using Escola.Infra.IoC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Escola.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController(IUsuarioService usuarioService, IAuthenticateService authenticateService) : Controller
{
    private readonly IUsuarioService _usuarioService = usuarioService;
    private readonly IAuthenticateService _authenticateService = authenticateService;

    [HttpPost]
    public async Task<ActionResult> CriarUsuario(UsuarioPostDTO usuario)
    {
        var usuarioCriado = await _usuarioService.AddAsync(usuario);
        var token = _authenticateService.GerarToken(usuarioCriado.Id, usuarioCriado.Email.ToLower(), usuarioCriado.Perfil);
        return Ok(new { Token = token, Nome = usuarioCriado.Nome });
    }

    [HttpPost("login")]
    public async Task<ActionResult> GetTokenUsuario(UserLogin usuario)
    {
        var usuarioExistente = await _authenticateService.AuthenticateAsync(usuario.Email, usuario.Senha);
        var token = _authenticateService.GerarToken(usuarioExistente.Id, usuarioExistente.Email.ToLower(), usuarioExistente.Perfil);
        return Ok(new { Token = token, Nome = usuarioExistente.Nome });
    }

    [HttpGet]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult> GetAllUsuarios([FromQuery] PaginationParams paginationParams)
    {
        var usuarios = await _usuarioService.GetAllAsync(paginationParams.PageNumber, paginationParams.PageSize);
        Response.AddPaginationHeader(new PaginationHeader(usuarios.CurrentPage, usuarios.TotalPages, usuarios.PageSize, usuarios.TotalCount));
        return Ok(usuarios);
    }

    [HttpPut]
    [Authorize]
    public async Task<ActionResult> UpdateUsuario(UsuarioPutDTO usuario)
    {
        await _usuarioService.UpdateAsync(User.GetUsuarioId(), usuario);
        return Ok(new { message = "Usuário atualizado com sucesso" });
    }
}
