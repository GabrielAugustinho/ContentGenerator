using ContentGenerator.Api.Core.InputPort.ShippingAccounts;

namespace ContentGenerator.Api.Core.UseCases.ShippingAccounts
{
    public interface IAddShippingAccounts
    {
        Task<bool> Execute(AddShippingAccountsInput input);
    }
}
