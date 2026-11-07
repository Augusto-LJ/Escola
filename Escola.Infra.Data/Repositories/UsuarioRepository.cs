using Escola.Domain.Entities;
using Escola.Domain.Interfaces;
using Escola.Domain.Pagination;
using Escola.Infra.Data.Context;
using Escola.Infra.Data.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Escola.Infra.Data.Repositories;

public class UsuarioRepository(ApplicationDbContext context) : IUsuarioRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Usuario> AddAsync(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return usuario;
    }

    public async Task<Usuario> DeleteAsync(int id)
    {
        var usuario = await _context.Usuarios.Where(x => x.Ativo).FirstOrDefaultAsync(c => c.Id == id);

        if (usuario is null)
            return null;

        usuario.Ativo = false;
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();

        return usuario;
    }

    public async Task<bool> ExisteUsuarioAsync()
    {
        return await _context.Usuarios.AnyAsync(x => x.Ativo);
    }

    public async Task<PagedList<Usuario>> GetAllAsync(int pageNumber, int pageSize)
    {
        var query = _context.Usuarios.Where(x => x.Ativo).AsNoTracking();
        return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
    }

    public async Task<Usuario> GetByIdAsync(int id)
    {
        return await _context.Usuarios.Where(x => x.Ativo).FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Usuario> GetUsuarioByEmail(string email)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower() && u.Ativo);
    }

    public async Task<Usuario> UpdateAsync(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();

        return usuario;
    }

    public async Task<bool> UsuarioExiste(string email)
    {
        return await _context.Usuarios
            .AnyAsync(u => u.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase) && u.Ativo);
    }
}
