using Escola.Domain.Entities;

namespace Escola.Domain.Interfaces;

public interface ICodigoRecuperacaoSenhaRepository
{
    Task AddAsync(CodigoRecuperacaoSenha codigoRecuperacaoSenha);
    Task<CodigoRecuperacaoSenha> GetAtivoByUsuarioIdAsync(int usuarioId);
    Task InvalidarAtivosAsync(int usuarioId);
    Task UpdateAsync(CodigoRecuperacaoSenha codigoRecuperacaoSenha);
}
