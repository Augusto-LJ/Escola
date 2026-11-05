using Escola.Domain.Entities;
using Escola.Domain.Interfaces;
using Escola.Domain.Pagination;
using Escola.Infra.Data.Context;
using Escola.Infra.Data.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Escola.Infra.Data.Repositories;

public class CursoRepository(ApplicationDbContext context) : ICursoRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Curso> AddAsync(Curso curso)
    {
        _context.Cursos.Add(curso);
        await _context.SaveChangesAsync();

        return curso;
    }

    public async Task<Curso> DeleteAsync(int id)
    {
        var curso = await _context.Cursos.Where(x => x.Ativo).FirstOrDefaultAsync(c => c.Id == id);

        if (curso is null)
            return null;

        curso.Ativo = false;
        _context.Cursos.Update(curso);
        await _context.SaveChangesAsync();

        return curso;
    }

    public async Task<PagedList<Curso>> GetAllAsync(int pageNumber, int pageSize)
    {
        var query = _context.Cursos.Where(x => x.Ativo).AsNoTracking();
        return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
    }

    public async Task<Curso> GetByIdAsync(int id)
    {
        return await _context.Cursos.Where(x => x.Ativo).FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Curso> UpdateAsync(Curso curso)
    {
        _context.Cursos.Update(curso);
        await _context.SaveChangesAsync();

        return curso;
    }
}
