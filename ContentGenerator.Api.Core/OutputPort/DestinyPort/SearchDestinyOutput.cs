using System.Diagnostics.CodeAnalysis;

namespace ContentGenerator.Api.Core.OutputPort.DestinyPort
{
    [ExcludeFromCodeCoverage]
    public class SearchDestinyOutput(int id, string descricao)
    {
        public int Id { get; set; } = id;
        public string Descricao { get; set; } = descricao;
    }
}
