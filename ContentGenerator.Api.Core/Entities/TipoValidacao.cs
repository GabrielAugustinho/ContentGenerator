using System.ComponentModel.DataAnnotations;

namespace ContentGenerator.Api.Core.Entities
{
    public class TipoValidacao
    {
        [Key()]
        public int TipoValidacaoId { get; set; }
        public required string Tipo { get; set; }
    }
}
