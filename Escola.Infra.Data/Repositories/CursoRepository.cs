using Escola.Domain.Entities;
using Escola.Domain.Interfaces;
using Escola.Infra.Data.Context;
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

    public async Task<List<Curso>> GetAllAsync()
    {
        return await _context.Cursos.Where(x => x.Ativo).ToListAsync();
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
