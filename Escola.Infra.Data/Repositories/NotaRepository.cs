using Escola.Domain.Entities;
using Escola.Domain.Interfaces;
using Escola.Infra.Data.Context;
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

    public async Task<List<Nota>> GetAllAsync()
    {
        return await _context.Notas.Where(x => x.Ativo).ToListAsync();
    }

    public async Task<Nota> GetByIdAsync(int id)
    {
        return await _context.Notas.Where(x => x.Ativo).FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Nota> UpdateAsync(Nota nota)
    {
        _context.Notas.Update(nota);
        await _context.SaveChangesAsync();

        return nota;
    }
}
