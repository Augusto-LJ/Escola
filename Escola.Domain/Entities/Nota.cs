namespace Escola.Domain.Entities;

public class Nota
{
    public int Id { get; set; }
    public int MatriculaId { get; set; }
    public bool Ativo { get; set; }
    public int ValorNota { get; set; }
    public bool Aprovado { get; set; }
    public DateTime DataNota { get; set; }
    public Matricula Matricula { get; set; }
}
