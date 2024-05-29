namespace ContentGenerator.Api.Core.OutputPort.EventPort
{
    public class SearchEventTypeOutput(int id, string descricao)
    {
        public int Id { get; set; } = id;
        public string Descricao { get; set; } = descricao;
    }
}
