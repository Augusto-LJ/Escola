using Escola.Application.DTOs.Usuario;
using Escola.Application.Exceptions;
using Escola.Application.Interfaces;
using Escola.Domain.Account;
using Escola.Domain.Entities;
using Escola.Domain.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Escola.Application.Services;

public class AuthenticateService(IUsuarioRepository usuarioRepository, IAuthenticate authenticate) : IAuthenticateService
{
    public readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
    public readonly IAuthenticate _authenticate = authenticate;

    public async Task<UsuarioGetDTO> AuthenticateAsync(string email, string senha)
    {
        var usuario = await _usuarioRepository.GetUsuarioByEmail(email);

        if (usuario == null || !usuario.Ativo)
            throw new BadRequestException("Usuário inválido ou inativo.");

        using var hmac = new HMACSHA512(usuario.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(senha));

        if (!computedHash.SequenceEqual(usuario.PasswordHash))
            throw new BadRequestException("Usuário ou senha inválidos.");

        return new UsuarioGetDTO
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Perfil = usuario.Perfil
        };
    }

    public string GerarToken(int id, string email, string role)
    {
        return _authenticate.GerarToken(id, email, role);
    }
}
