using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContentGenerator.Api.Core.Entities
{
    public class Publicacao
    {
        [Key()]
        public int PublicacaoId { get; set; }

        [ForeignKey("Assunto")]
        public required int AssuntoId { get; set; }
        public virtual Assunto Assunto { get; set; }


        [ForeignKey("Destinos")]
        public required int DestinosId { get; set; }
        public virtual Destinos Destino { get; set; }
    }
}
