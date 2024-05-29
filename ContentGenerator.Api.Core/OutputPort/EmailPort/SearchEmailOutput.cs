using System.Diagnostics.CodeAnalysis;

namespace ContentGenerator.Api.Core.OutputPort.EmailPort
{
    [ExcludeFromCodeCoverage]
    public class SearchEmailOutput(int id, string? nome, string? email, bool ativo, int totalCount)
    {
        public int Id { get; set; } = id;
        public string? Nome { get; set; } = nome;
        public string? Email { get; set; } = email;
        public bool Ativo { get; set; } = ativo;
        public int? TotalCount { get; set; } = totalCount;
    }
}
