using Escola.API.Models;
using Escola.Application.DTOs.Usuario;
using Escola.Application.Interfaces;
using Escola.Domain.Account;
using Microsoft.AspNetCore.Mvc;

namespace Escola.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController(IUsuarioService usuarioService, IAuthenticate authenticate) : Controller
{
    private readonly IUsuarioService _usuarioService = usuarioService;
    private readonly IAuthenticate _authenticate = authenticate;

    [HttpPost]
    public async Task<ActionResult> CriarUsuario(UsuarioPostDTO usuario)
    {
        var usuarioCriado = await _usuarioService.AddAsync(usuario);
        var token = _authenticate.GerarToken(usuarioCriado.Id, usuarioCriado.Email.ToLower(), usuarioCriado.Perfil);
        return Ok(new { Token = token, Nome = usuarioCriado.Nome });
    }

    [HttpPost("login")]
    public async Task<ActionResult> GetTokenUsuario(UserLogin usuario)
    {
        var usuarioExistente = await _authenticate.GetUsuarioByEmail(usuario.Email);
        if (usuarioExistente == null)
            return BadRequest(new { Message = "Usuário ou senha inválidos." });

        var conseguiuAutenticar = await _authenticate.AuthenticateAsync(usuario.Email, usuario.Senha);
        if (!conseguiuAutenticar)
            return BadRequest(new { Message = "Usuário ou senha inválidos." });

        var token = _authenticate.GerarToken(usuarioExistente.Id, usuarioExistente.Email.ToLower(), usuarioExistente.Perfil);
        return Ok(new { Token = token, Nome = usuarioExistente.Nome });
    }
}
