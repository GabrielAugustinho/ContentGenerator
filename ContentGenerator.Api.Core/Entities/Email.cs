using System.ComponentModel.DataAnnotations;

namespace ContentGenerator.Api.Core.Entities
{
    public class Email
    {
        [Key()]
        public int EmailId { get; set; }
        public required string NomeCliente { get; set; }
        public required string EmailCliente { get; set; }
        public bool Ativo { get; set; } = true;

        public void Update(string nome, string email)
        {
            NomeCliente = nome;
            EmailCliente = email;
        }

        public void Delete()
        {
            Ativo = false;
        }
    }
}
