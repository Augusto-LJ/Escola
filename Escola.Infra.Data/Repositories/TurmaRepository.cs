using Escola.Domain.Entities;
using Escola.Domain.Interfaces;
using Escola.Domain.Pagination;
using Escola.Infra.Data.Context;
using Escola.Infra.Data.Helpers;
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

    public async Task<PagedList<Turma>> GetAllAsync(int pageNumber, int pageSize)
    {
        var query = _context.Turmas
            .Include(x => x.Curso)
            .Where(x => x.Ativo)
            .AsNoTracking();

        return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
    }

    public async Task<Turma> GetByIdAsync(int id)
    {
        return await _context.Turmas
            .Include(x => x.Curso)
            .Where(x => x.Ativo)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<PagedList<Turma>> GetTurmasByUsuarioId(int usuarioId, int pageNumber, int pageSize)
    {
        var query = _context.Turmas
            .Include(x => x.Curso)
            .Where(x => x.Ativo && x.Matriculas.Any(m => m.UsuarioId == usuarioId))
            .AsNoTracking();

        return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
    }

    public async Task<Turma> UpdateAsync(Turma turma)
    {
        _context.Turmas.Update(turma);
        await _context.SaveChangesAsync();

        return turma;
    }
}
