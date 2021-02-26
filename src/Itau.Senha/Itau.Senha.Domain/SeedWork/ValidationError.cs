namespace Itau.Senha.Domain.SeedWork
{
    public class ValidationError
    {
     
        public ValidationError(string property, string error)
        {
            Property = property;
            Error = error;
        }

        public string Property { get; set; }
        public string Error { get; set; }
    }
}
