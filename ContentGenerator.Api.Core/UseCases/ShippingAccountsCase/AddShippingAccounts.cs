using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.InputPort.ShippingAccountsPort;
using Microsoft.Extensions.Logging;

namespace ContentGenerator.Api.Core.UseCases.ShippingAccountsCase
{
    public class AddShippingAccounts : IAddShippingAccounts
    {
        private readonly IShippingAccountsRepository _shippingAccountsRepository;
        private readonly ILogger<AddShippingAccounts> _logger;

        public AddShippingAccounts(IShippingAccountsRepository shippingAccountsRepository, ILogger<AddShippingAccounts> logger)
        {
            _shippingAccountsRepository = shippingAccountsRepository;
            _logger = logger;
        }

        public async Task<bool> Execute(AddShippingAccountsInput input)
        {
            _logger.LogInformation("Starting the process to update shipping accounts.");

            var result = await _shippingAccountsRepository.DeleteAccounts(input.DestinosId);
            if (!result)
            {
                _logger.LogWarning("Failed to delete existing shipping accounts for DestinosId {DestinosId}.", input.DestinosId);
                return false;
            }

            _logger.LogInformation("Existing shipping accounts deleted successfully for DestinosId {DestinosId}.", input.DestinosId);

            result = await _shippingAccountsRepository.AddAccounts(input);

            if (result)
            {
                _logger.LogInformation("Shipping accounts added successfully for DestinosId {DestinosId}.", input.DestinosId);
            }
            else
            {
                _logger.LogWarning("Failed to add new shipping accounts for DestinosId {DestinosId}.", input.DestinosId);
            }

            return result;
        }
    }
}