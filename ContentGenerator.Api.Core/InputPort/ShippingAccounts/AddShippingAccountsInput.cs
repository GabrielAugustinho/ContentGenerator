using System.ComponentModel.DataAnnotations.Schema;

namespace ContentGenerator.Api.Core.InputPort.ShippingAccounts
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
