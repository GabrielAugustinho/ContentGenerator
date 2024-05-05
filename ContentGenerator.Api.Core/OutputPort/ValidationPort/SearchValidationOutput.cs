namespace ContentGenerator.Api.Core.OutputPort.ValidationPort
{
    public class SearchValidationOutput(int id, string tipo)
    {
        public int Id { get; set; } = id;
        public string Tipo { get; set; } = tipo;    
    }
}
