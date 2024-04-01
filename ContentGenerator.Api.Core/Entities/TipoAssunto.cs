using System.ComponentModel.DataAnnotations;

namespace ContentGenerator.Api.Core.Entities
{
    public class TipoAssunto
    {
        [Key()]
        public int TipoAssuntoId { get; set; }
        public required string Assunto { get; set; }

        public void Update(string descricao)
        {
            Assunto = descricao;
        }
    }
}
