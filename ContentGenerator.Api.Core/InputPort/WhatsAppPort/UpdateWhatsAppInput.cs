namespace ContentGenerator.Api.Core.InputPort.WhatsAppPort
{
    public class UpdateWhatsAppInput
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required long NumeroFone { get; set; }
    }
}
