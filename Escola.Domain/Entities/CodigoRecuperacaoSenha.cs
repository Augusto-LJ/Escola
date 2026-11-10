namespace Escola.Domain.Entities;

public class CodigoRecuperacaoSenha
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public byte[] CodigoHash { get; set; }
    public byte[] CodigoSalt { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime ExpiraEm { get; set; }
    public bool JaFoiUsado { get; set; }
    public int TentativasRestantes { get; set; }

    public Usuario Usuario { get; set; }
}