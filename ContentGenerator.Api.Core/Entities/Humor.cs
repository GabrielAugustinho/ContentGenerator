using System.ComponentModel.DataAnnotations;

namespace ContentGenerator.Api.Core.Entities
{
    public class Humor
    {
        [Key()]
        public int HumorId { get; set; }
        public required string Descricao { get; set; }
    }
}
