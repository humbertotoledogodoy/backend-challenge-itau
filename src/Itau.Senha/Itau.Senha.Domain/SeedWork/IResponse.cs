using System.Collections.Generic;

namespace Itau.Senha.Domain.SeedWork
{
    public interface IResponse
    {
        bool Sucesso { get; }
        void AddError(ValidationError error);
        void AddErros(IEnumerable<ValidationError> erros);
        IReadOnlyList<ValidationError> Errors { get; }
    }
}
