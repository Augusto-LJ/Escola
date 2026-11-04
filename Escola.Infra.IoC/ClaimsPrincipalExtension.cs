using System.Security.Claims;

namespace Escola.Infra.IoC;

public static class ClaimsPrincipalExtension
{
    public static int GetUsuarioId(this ClaimsPrincipal user)
    {
        var usuarioIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        if (usuarioIdClaim == null || !int.TryParse(usuarioIdClaim.Value, out int userId))
            throw new Exception("Claim 'NameIdentifier' não encontrada ou inválida.");

        return userId;
    }
}
