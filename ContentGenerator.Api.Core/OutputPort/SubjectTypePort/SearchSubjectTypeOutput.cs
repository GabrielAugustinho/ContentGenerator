namespace ContentGenerator.Api.Core.OutputPort.SubjectTypePort
{
    public class SearchSubjectTypeOutput(int id, string descricao)
    {
        public int Id { get; set; } = id;
        public string Descricao { get; set; } = descricao;
    }
}
