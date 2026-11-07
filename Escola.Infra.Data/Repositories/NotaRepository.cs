using Escola.Domain.Entities;
using Escola.Domain.Interfaces;
using Escola.Domain.Pagination;
using Escola.Infra.Data.Context;
using Escola.Infra.Data.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Escola.Infra.Data.Repositories;

public class NotaRepository(ApplicationDbContext context) : INotaRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Nota> AddAsync(Nota nota)
    {
        _context.Notas.Add(nota);
        await _context.SaveChangesAsync();

        return nota;
    }

    public async Task<Nota> DeleteAsync(int id)
    {
        var nota = await _context.Notas.Where(x => x.Ativo).FirstOrDefaultAsync(c => c.Id == id);

        if (nota is null)
            return null;

        nota.Ativo = false;
        _context.Notas.Update(nota);
        await _context.SaveChangesAsync();

        return nota;
    }

    public async Task<PagedList<Nota>> GetAllAsync(int pageNumber, int pageSize)
    {
        var query = _context.Notas.Where(x => x.Ativo).AsNoTracking();
        return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
    }

    public async Task<Nota> GetByIdAsync(int id)
    {
        return await _context.Notas.Where(x => x.Ativo).FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<PagedList<Nota>> GetNotasByTurmaUsuarioId(int turmaId, int usuarioId, int pageNumber, int pageSize)
    {
        var query = _context.Notas
            .Include(n => n.Matricula)
            .Where(n => n.Ativo && n.Matricula.TurmaId == turmaId && n.Matricula.UsuarioId == usuarioId)
            .AsNoTracking();

        return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);

    }

    public async Task<Nota> UpdateAsync(Nota nota)
    {
        _context.Notas.Update(nota);
        await _context.SaveChangesAsync();

        return nota;
    }
}
