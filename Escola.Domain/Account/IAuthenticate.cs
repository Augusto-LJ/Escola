using Escola.Domain.Entities;

namespace Escola.Domain.Account;

public interface IAuthenticate
{
    string GerarToken(int id, string email, string role);
}
