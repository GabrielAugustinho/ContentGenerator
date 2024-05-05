namespace ContentGenerator.Api.Core.OutputPort.HumorPort
{
    public class HumorOutput(int id, string descricao)
    {
        public int Id { get; set; } = id;
        public string Descricao { get; set; } = descricao;
    }
}
