using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Itau.Senha.Domain.SeedWork
{
    public abstract class BaseResponse : IResponse
    {
        private readonly List<ValidationError> errors;
        public BaseResponse()
        {
            errors = new List<ValidationError>();
        }

        [JsonIgnore]
        public bool Sucesso => Errors.Count == 0;

        public IReadOnlyList<ValidationError> Errors => errors as IReadOnlyList<ValidationError>;
        public void AddError(ValidationError error) => errors.Add(error);
        public void AddErros(IEnumerable<ValidationError> errors) => this.errors.AddRange(errors);

    }
}
