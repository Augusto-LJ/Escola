using Escola.Domain.Entities;
using Escola.Domain.Interfaces;
using Escola.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Escola.Infra.Data.Repositories;

public class CodigoRecuperacaoSenhaRepository(ApplicationDbContext context) : ICodigoRecuperacaoSenhaRepository
{
    private readonly ApplicationDbContext _context = context;
    public async Task AddAsync(CodigoRecuperacaoSenha codigoRecuperacaoSenha)
    {
        _context.CodigosRecuperacaoSenha.Add(codigoRecuperacaoSenha);
        await _context.SaveChangesAsync();
    }

    public async Task<CodigoRecuperacaoSenha> GetAtivoByUsuarioIdAsync(int usuarioId)
    {
        return await _context.CodigosRecuperacaoSenha
            .Where(c => c.UsuarioId == usuarioId && !c.JaFoiUsado && c.ExpiraEm > DateTime.UtcNow)
            .OrderByDescending(c => c.CriadoEm)
            .FirstOrDefaultAsync();   
    }

    public async Task InvalidarAtivosAsync(int usuarioId)
    {
        await _context.CodigosRecuperacaoSenha
            .Where(c => c.UsuarioId == usuarioId && !c.JaFoiUsado)
            .ExecuteUpdateAsync(setters => setters.SetProperty(c => c.JaFoiUsado, true));
    }

    public async Task UpdateAsync(CodigoRecuperacaoSenha codigoRecuperacaoSenha)
    {
        _context.CodigosRecuperacaoSenha.Update(codigoRecuperacaoSenha);
        await _context.SaveChangesAsync();
    }
}
