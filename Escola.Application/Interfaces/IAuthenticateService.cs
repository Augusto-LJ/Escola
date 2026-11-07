using Escola.Application.DTOs.Usuario;
using Escola.Domain.Entities;

namespace Escola.Application.Interfaces;

public interface IAuthenticateService
{
    Task<UsuarioGetDTO> AuthenticateAsync(string email, string senha);
    string GerarToken(int id, string email, string role);
}
