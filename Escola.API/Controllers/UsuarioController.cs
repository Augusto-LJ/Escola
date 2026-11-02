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
}
