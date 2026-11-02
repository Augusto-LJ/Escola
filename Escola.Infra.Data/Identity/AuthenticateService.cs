using Escola.Domain.Account;
using Escola.Domain.Entities;
using Escola.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Escola.Infra.Data.Identity;

public class AuthenticateService(ApplicationDbContext context, IConfiguration configuration) : IAuthenticate
{
    private readonly ApplicationDbContext _context = context;
    private readonly IConfiguration _configuration = configuration;

    public async Task<bool> AuthenticateAsync(string email, string senha)
    {
        var usuario = await GetUsuarioByEmail(email);
        if (usuario == null || !usuario.Ativo)
            return false;

        using var hmac = new HMACSHA512(usuario.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(senha));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != usuario.PasswordHash[i])
                return false;
        }

        return true;
    }

    public string GerarToken(int id, string email, string role)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, id.ToString()),
            new Claim(ClaimTypes.Email, email.ToLower()),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
        var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<Usuario> GetUsuarioByEmail(string email)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower() && u.Ativo);
    }

    public Task<bool> UsuarioExiste(string email)
    {
        throw new NotImplementedException();
    }
}
