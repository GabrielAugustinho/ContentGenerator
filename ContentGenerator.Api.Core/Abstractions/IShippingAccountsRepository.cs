using ContentGenerator.Api.Core.InputPort.ShippingAccountsPort;

namespace ContentGenerator.Api.Core.Abstractions
{
    public interface IShippingAccountsRepository
    {
        Task<bool> AddAccounts(AddShippingAccountsInput input);
        Task<bool> DeleteAccounts(int destinosId);
    }
}
