using Escola.Domain.Entities;
using Escola.Domain.Interfaces;
using Escola.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Escola.Infra.Data.Repositories;

public class TurmaRepository(ApplicationDbContext context) : ITurmaRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Turma> AddAsync(Turma turma)
    {
        _context.Turmas.Add(turma);
        await _context.SaveChangesAsync();

        return turma;
    }

    public async Task<Turma> DeleteAsync(int id)
    {
        var turma = await _context.Turmas.Where(x => x.Ativo).FirstOrDefaultAsync(c => c.Id == id);

        if (turma is null)
            return null;

        turma.Ativo = false;
        _context.Turmas.Update(turma);
        await _context.SaveChangesAsync();

        return turma;
    }

    public async Task<List<Turma>> GetAllAsync()
    {
        return await _context.Turmas
            .Include(x => x.Curso)
            .Where(x => x.Ativo)
            .ToListAsync();
    }

    public async Task<Turma> GetByIdAsync(int id)
    {
        return await _context.Turmas
            .Include(x => x.Curso)
            .Where(x => x.Ativo)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Turma> UpdateAsync(Turma turma)
    {
        _context.Turmas.Update(turma);
        await _context.SaveChangesAsync();

        return turma;
    }
}
