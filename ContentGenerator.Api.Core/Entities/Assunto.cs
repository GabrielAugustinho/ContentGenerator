using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContentGenerator.Api.Core.Entities
{
    public class Assunto
    {
        [Key()]
        public int AssuntoId { get; set; }

        [ForeignKey("TipoValidacao")]
        public required int TipoValidacaoId { get; set; }
        public virtual TipoValidacao TipoValidacao { get; set; }

        [ForeignKey("Humor")]
        public required int HumorId { get; set; }
        public virtual Humor Humor { get; set; }

        [ForeignKey("Destinos")]
        public required int DestinosId { get; set; }
        public virtual Destinos Destinos { get; set; }

        [ForeignKey("TipoAssunto")]
        public required int TipoAssuntoId { get; set; }
        public virtual TipoAssunto TipoAssunto { get; set; }

        public required DateTime DataCriacao { get; set; } // Data da criação do assunto, criei hoje para postar amanhã
        public required string ObjEveAssunto { get; set; } // o que o usuario inseriu, descrição do que ele quer
        public DateTime? DataGeracao { get; set; } // Data da geração do texto
        public string? PostOriginal { get; set; } // Post gerado pelo GPT
        public DateTime? DataValida { get; set; }
        public string? PostValidado { get; set; }
        public DateTime? DataPublicacao { get; set; }
        public string? ImagemPost { get; set; }
        public bool IncluirImg { get; set; } = false;
        public bool Ativo { get; set; } = true;
    }
}
