using ContentGenerator.Api.Core.OutputPort.DestinyPort;
using ContentGenerator.Api.Core.OutputPort.HumorPort;
using ContentGenerator.Api.Core.OutputPort.ValidationPort;

namespace ContentGenerator.Api.Core.OutputPort.EventPort
{
    public class SearchEventOutput
    {
        public SearchEventOutput(int id, SearchDestinyOutput destino, SearchHumorOutput tipoHumor, SearchValidationOutput tipoValidacao, SearchEventTypeOutput tipoEvento, int dia, int mes, int ano, string objEveAssunto, string descricao, bool ativo)
        {
            Id = id;
            Destino = destino;
            TipoHumor = tipoHumor;
            TipoValidacao = tipoValidacao;
            TipoEvento = tipoEvento;
            Dia = dia;
            Mes = mes;
            Ano = ano;
            DescricaoUsuario = objEveAssunto;
            Descricao = descricao;
            Ativo = ativo;
        }

        public int Id { get; set; }
        public SearchDestinyOutput Destino { get; set; }
        public SearchHumorOutput TipoHumor { get; set; }
        public SearchValidationOutput TipoValidacao { get; set; }
        public SearchEventTypeOutput TipoEvento { get; set; }
        public int Dia { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public string DescricaoUsuario { get; set; } // o que o usuario inseriu, descrição do que ele quer
        public string Descricao { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
