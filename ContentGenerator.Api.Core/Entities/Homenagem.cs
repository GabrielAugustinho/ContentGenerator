using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContentGenerator.Api.Core.Entities
{
    public class Homenagem
    {
        [Key()]
        public int HomenagemId { get; set; }

        [ForeignKey("Destinos")]
        public required int DestinosId { get; set; }
        public virtual Destinos Destinos { get; set; }

        [ForeignKey("Humor")]
        public required int HumorId { get; set; }
        public virtual Humor Humor { get; set; }

        [ForeignKey("TipoValidacao")]
        public required int TipoValidacaoId { get; set; }
        public virtual TipoValidacao TipoValidacao { get; set; }

        [ForeignKey("TipoHomenagem")]
        public required int TipoHomenagemId { get; set; }
        public virtual TipoHomenagem TipoHomenagem { get; set; }

        public required int Dia { get; set; }
        public required int Mes { get; set; }
        public required int Ano { get; set; }
        public required string ObjEveAssunto { get; set; } // o que o usuario inseriu, descrição do que ele quer
        public required string Descricao { get; set; }
        public bool Ativo { get; set; } = true;

        public void Update(Destinos destino, Humor humor, TipoValidacao tipoValidacao, TipoHomenagem tipoHomenagem, int dia, int mes, int ano, string objEveAssunto)
        {
            DestinosId = destino.DestinosId;
            Destinos = destino;
            HumorId = humor.HumorId;
            Humor = humor;
            TipoValidacaoId = tipoValidacao.TipoValidacaoId;
            TipoValidacao = tipoValidacao;
            TipoHomenagemId = tipoHomenagem.TipoHomenagemId;
            TipoHomenagem = tipoHomenagem;
            Dia = dia;
            Mes = mes;
            Ano = ano;
            ObjEveAssunto = objEveAssunto;
            Descricao = destino.Descricao;
        }

        public void Delete()
        {
            Ativo = false;
        }
    }
}
