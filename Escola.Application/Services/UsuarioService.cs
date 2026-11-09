namespace Escola.Application.Services;

using Escola.Application.DTOs.Usuario;
using Escola.Application.Exceptions;
using Escola.Application.Interfaces;
using Escola.Domain.Entities;
using Escola.Domain.Interfaces;
using Escola.Domain.Pagination;
using System.Security.Cryptography;
using System.Text;

public class UsuarioService(IUsuarioRepository usuarioRepository) : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;

    public async Task<UsuarioGetDTO> AddAsync(UsuarioPostDTO usuario)
    {
        var usuarioExiste = await _usuarioRepository.UsuarioExiste(usuario.Email);

        if (usuarioExiste)
            throw new BadRequestException("Já existe um usuário vinculado a este e-mail");

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
            Email = usuarioCriado.Email,
            Perfil = usuarioCriado.Perfil
        };
    }

    public async Task<UsuarioGetDTO> DeleteAsync(int id)
    {
        var usuarioDeletado = await _usuarioRepository.DeleteAsync(id);

        if (usuarioDeletado == null)
            throw new NotFoundException("Usuário não encontrado.");

        return new UsuarioGetDTO
        {
            Id = usuarioDeletado.Id,
            Nome = usuarioDeletado.Nome,
            Email = usuarioDeletado.Email,
            Perfil = usuarioDeletado.Perfil
        };
    }

    public async Task<bool> ExisteUsuarioAsync()
    {
        return await _usuarioRepository.ExisteUsuarioAsync();
    }

    public async Task<PagedList<UsuarioGetDTO>> GetAllAsync(int pageNumber, int pageSize)
    {
        var todosUsuarios = await _usuarioRepository.GetAllAsync(pageNumber, pageSize);

        var usuariosDTO = new List<UsuarioGetDTO>();

        usuariosDTO.AddRange(todosUsuarios.Select(u => new UsuarioGetDTO
        {
            Id = u.Id,
            Nome = u.Nome,
            Email = u.Email
        }));

        return new PagedList<UsuarioGetDTO>(usuariosDTO, todosUsuarios.CurrentPage, todosUsuarios.PageSize, todosUsuarios.TotalCount);
    }

    public async Task<UsuarioGetDTO> GetByIdAsync(int id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);

        if (usuario == null)
            throw new NotFoundException("Usuário não encontrado.");

        return new UsuarioGetDTO
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Perfil = usuario.Perfil
        };
    }

    public async Task<UsuarioGetDTO> GetUsuarioByEmailAsync(string email)
    {
        var usuario = await _usuarioRepository.GetUsuarioByEmail(email);
        
        if (usuario == null)
            throw new NotFoundException("Usuário não encontrado.");

        return new UsuarioGetDTO
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Perfil = usuario.Perfil
        };
    }

    public async Task<UsuarioGetDTO> UpdateAsync(int usuarioId, UsuarioPutDTO usuario)
    {
        var usuarioParaAtualizar = await _usuarioRepository.GetByIdAsync(usuarioId) ?? throw new NotFoundException("Usuário não encontrado.");

        if (!usuario.Email.Equals(usuarioParaAtualizar.Email, StringComparison.CurrentCultureIgnoreCase))
        {
            var usuarioExiste = await _usuarioRepository.UsuarioExiste(usuario.Email);

            if (usuarioExiste)
                throw new BadRequestException("Já existe um usuário vinculado a este e-mail");

            usuarioParaAtualizar.Email = usuario.Email;
        }

        usuarioParaAtualizar.Nome = usuario.Nome;

        var usuarioAtualizado = await _usuarioRepository.UpdateAsync(usuarioParaAtualizar) ?? throw new NotFoundException("Usuário não encontrado.");

        return new UsuarioGetDTO
        {
            Id = usuarioAtualizado.Id,
            Nome = usuarioAtualizado.Nome,
            Email = usuarioAtualizado.Email,
            Perfil = usuarioAtualizado.Perfil
        };
    }

    public async Task AlterarSenhaAsync(int usuarioId, AlterarSenhaUsuarioDTO alterarSenhaUsuarioDto)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(usuarioId);

        if (usuario == null || !usuario.Ativo)
            throw new NotFoundException("Usuário não encontrado.");

        using var hmac = new HMACSHA512(usuario.PasswordSalt);
        var hashInformado = hmac.ComputeHash(Encoding.UTF8.GetBytes(alterarSenhaUsuarioDto.SenhaAtual));

        if (!hashInformado.SequenceEqual(usuario.PasswordHash))
            throw new BadRequestException("Senha atual incorreta.");

        using var hmacNovo = new HMACSHA512();
        usuario.PasswordHash = hmacNovo.ComputeHash(Encoding.UTF8.GetBytes(alterarSenhaUsuarioDto.NovaSenha));
        usuario.PasswordSalt = hmacNovo.Key;
        await _usuarioRepository.UpdateAsync(usuario);
    }
}
