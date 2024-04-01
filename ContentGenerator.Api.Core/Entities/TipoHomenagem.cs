using System.ComponentModel.DataAnnotations;

namespace ContentGenerator.Api.Core.Entities
{
    // Seria TipoEventos porém quem modelou o banco quis TipoHomenagem e não pode argumentar :| ...
    public class TipoHomenagem
    {
        [Key()]
        public int TipoHomenagemId { get; set; }
        public required string Descricao { get; set; }

        public void Update(string descricao)
        {
            Descricao = descricao;
        }
    }
}
