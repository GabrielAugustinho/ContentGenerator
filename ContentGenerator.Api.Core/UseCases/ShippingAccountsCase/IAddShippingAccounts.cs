using ContentGenerator.Api.Core.InputPort.ShippingAccountsPort;

namespace ContentGenerator.Api.Core.UseCases.ShippingAccountsCase
{
    public interface IAddShippingAccounts
    {
        Task<bool> Execute(AddShippingAccountsInput input);
    }
}
