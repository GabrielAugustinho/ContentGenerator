namespace ContentGenerator.Api.Core.InputPort.ContentPort
{
    public class ContentInput
    {
        public required int TipoValidacaoId { get; set; }
        public required int HumorId { get; set; }
        public required int DestinosId { get; set; }
        public required int TipoAssuntoId { get; set; }
        public required DateTime DataCriacao { get; set; } // Data da criação do assunto, criei hoje para postar amanhã
        public string? ObjEveAssunto { get; set; } // o que o usuario inseriu, descrição do que ele quer
        public required DateTime DataGeracao { get; set; } // Data da geração do texto
        public required string PostOriginal { get; set; } // Post gerado pelo GPT
        public required string ImagemPost { get; set; }
    }
}
