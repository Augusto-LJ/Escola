using Escola.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Escola.Infra.Data.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Curso> Cursos { get; set; }
    public DbSet<Matricula> Matriculas { get; set; }
    public DbSet<Nota> Notas { get; set; }
    public DbSet<Turma> Turmas { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<CodigoRecuperacaoSenha> CodigosRecuperacaoSenha { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
