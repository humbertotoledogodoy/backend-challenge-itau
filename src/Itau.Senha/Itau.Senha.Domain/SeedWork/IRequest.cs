using System.Collections.Generic;

namespace Itau.Senha.Domain.SeedWork
{
    public interface IRequest
    {
        IEnumerable<ValidationError> Validate();
    }
}
