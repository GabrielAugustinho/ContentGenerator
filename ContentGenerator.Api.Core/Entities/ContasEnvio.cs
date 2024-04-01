using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContentGenerator.Api.Core.Entities
{
    public class ContasEnvio
    {
        [Key()]
        public int ContasEnvioId { get; set; }
        public required string Configuracao { get; set; }

        [ForeignKey("Destinos")]
        public required int DestinosId { get; set; }
        public virtual Destinos Destino { get; set; }
    }
}
