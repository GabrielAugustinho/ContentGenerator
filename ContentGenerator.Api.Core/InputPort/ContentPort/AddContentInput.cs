namespace ContentGenerator.Api.Core.InputPort.ContentPort
{
    public class AddContentInput
    {
        public required int HomenagemId { get; set; }
        public required string Homenagem { get; set; }

        public required int TipoValidacaoId { get; set; }
        public required string TipoValidacao { get; set; }

        public required int HumorId { get; set; }
        public required string Humor { get; set; }

        public required int DestinosId { get; set; }
        public required string Destinos { get; set; }

        public required int TipoAssuntoId { get; set; }
        public required string TipoAssunto { get; set; }

        public required DateTime DataPostagem { get; set; } // Data da criação do assunto, criei hoje para postar amanhã
        public string? DescricaoUsuario { get; set; } // o que o usuario inseriu, descrição do que ele quer
    }

}
