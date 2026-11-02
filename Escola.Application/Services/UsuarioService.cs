namespace Escola.Application.Services;

using Escola.Application.DTOs.Usuario;
using Escola.Application.Interfaces;
using Escola.Domain.Entities;
using Escola.Domain.Interfaces;
using System.Security.Cryptography;

public class UsuarioService(IUsuarioRepository usuarioRepository) : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;

    public async Task<UsuarioGetDTO> AddAsync(UsuarioPostDTO usuario)
    {
        using var hmac = new HMACSHA512();
        byte[] senhaHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(usuario.Senha));
        byte[] senhaSalt = hmac.Key;
        var existeAlgumUsuario = await _usuarioRepository.ExisteUsuarioAsync();

        var novoUsuario = new Usuario
        {
            Nome = usuario.Nome,
            Email = usuario.Email,
            Ativo = true,
            Perfil = existeAlgumUsuario ? "Aluno" : "Administrador",
            PasswordHash = senhaHash,
            PasswordSalt = senhaSalt
        };

        var usuarioCriado = await _usuarioRepository.AddAsync(novoUsuario);

        return new UsuarioGetDTO
        {
            Id = usuarioCriado.Id,
            Nome = usuarioCriado.Nome,
            Email = usuarioCriado.Email
        };
    }

    public async Task<UsuarioGetDTO> DeleteAsync(int id)
    {
        var usuarioDeletado = await _usuarioRepository.DeleteAsync(id);

        if (usuarioDeletado == null)
            return null;

        return new UsuarioGetDTO
        {
            Id = usuarioDeletado.Id,
            Nome = usuarioDeletado.Nome,
            Email = usuarioDeletado.Email
        };
    }

    public async Task<bool> ExisteUsuarioAsync()
    {
        return await _usuarioRepository.ExisteUsuarioAsync();
    }

    public async Task<List<UsuarioGetDTO>> GetAllAsync()
    {
        var todosUsuarios = await _usuarioRepository.GetAllAsync();

        var usuariosDTO = new List<UsuarioGetDTO>();

        usuariosDTO.AddRange(todosUsuarios.Select(u => new UsuarioGetDTO
        {
            Id = u.Id,
            Nome = u.Nome,
            Email = u.Email
        }));

        return usuariosDTO;
    }

    public async Task<UsuarioGetDTO> GetByIdAsync(int id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);

        if (usuario == null)
            return null;

        return new UsuarioGetDTO
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email
        };
    }

    public async Task<UsuarioGetDTO> UpdateAsync(int usuarioId, UsuarioPutDTO usuario)
    {
        var usuarioParaAtualizar = new Usuario
        {
            Id = usuarioId,
            Nome = usuario.Nome,
            Email = usuario.Email
        };

        var usuarioAtualizado = await _usuarioRepository.UpdateAsync(usuarioParaAtualizar);

        if (usuarioAtualizado == null)
            return null;

        return new UsuarioGetDTO
        {
            Id = usuarioAtualizado.Id,
            Nome = usuarioAtualizado.Nome,
            Email = usuarioAtualizado.Email
        };
    }
}
