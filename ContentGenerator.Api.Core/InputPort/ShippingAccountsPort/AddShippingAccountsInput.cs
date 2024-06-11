namespace ContentGenerator.Api.Core.InputPort.ShippingAccountsPort
{
    public class AddShippingAccountsInput
    {
        public required int DestinosId { get; set; }
        public required List<Accounts> Accounts { get; set; }
    }

    public class Accounts
    {
        public required string Configuracao { get; set; }
    }
}
