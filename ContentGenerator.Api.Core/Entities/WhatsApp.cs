using System.ComponentModel.DataAnnotations;

namespace ContentGenerator.Api.Core.Entities
{
    public class WhatsApp
    {
        [Key()]
        public int WhatsAppId { get; set; }
        public required string Nome { get; set; }
        public required long Numero_Fone { get; set; }
        public bool Ativo { get; set; } = true;

        public void Update(string nome, long numeroFone)
        {
            Nome = nome;
            Numero_Fone = numeroFone;
        }

        public void Delete()
        {
            Ativo = false;
        }
    }
}
