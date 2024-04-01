using System.ComponentModel.DataAnnotations;

namespace ContentGenerator.Api.Core.Entities
{
    public class Destinos
    {
        [Key()] 
        public int DestinosId { get; set; }
        public required string Descricao { get; set; }
    }
}
