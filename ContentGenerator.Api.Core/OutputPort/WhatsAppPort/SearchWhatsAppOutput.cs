using System.Diagnostics.CodeAnalysis;

namespace ContentGenerator.Api.Core.OutputPort.WhatsAppPort
{
    [ExcludeFromCodeCoverage]
    public class SearchWhatsAppOutput(int id, string? nome, long numeroFone, bool ativo, int totalCount)
    {
        public int Id { get; set; } = id;
        public string? Nome { get; set; } = nome;
        public long NumeroFone { get; set; } = numeroFone;
        public bool Ativo { get; set; } = ativo;
        public int? TotalCount { get; set; } = totalCount;
    }
}
