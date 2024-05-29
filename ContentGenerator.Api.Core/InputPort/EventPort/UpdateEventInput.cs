namespace ContentGenerator.Api.Core.InputPort.EventPort
{
    public class UpdateEventInput
    {
        public required int EventoId { get; set; }
        public required int DestinosId { get; set; }
        public required int HumorId { get; set; }
        public required int TipoValidacaoId { get; set; }
        public required int TipoEventoId { get; set; }
        public required int Dia { get; set; }
        public required int Mes { get; set; }
        public required int Ano { get; set; }
        public required string DescricaoUsuario { get; set; }
        public required string Descricao { get; set; }
    }
}
