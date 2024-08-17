using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Core.InputPort.ShippingAccountsPort;
using ContentGenerator.Api.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ContentGenerator.Api.Adapters.Repository
{
    public class ShippingAccountsRepository : IShippingAccountsRepository
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<ShippingAccountsRepository> _logger;

        public ShippingAccountsRepository(DataContext dataContext, ILogger<ShippingAccountsRepository> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        public async Task<bool> AddAccounts(AddShippingAccountsInput input)
        {
            _logger.LogInformation("Adding new shipping accounts.");

            if (input == null || input.Accounts == null || input.Accounts.Count == 0)
            {
                _logger.LogWarning("Invalid input or no accounts provided.");
                return false;
            }

            var contasEnvio = new List<ContasEnvio>();

            foreach (var configuration in input.Accounts)
            {
                if (string.IsNullOrWhiteSpace(configuration.Configuracao))
                {
                    _logger.LogWarning("Invalid configuration found for one of the accounts.");
                    return false;
                }

                contasEnvio.Add(new ContasEnvio
                {
                    Configuracao = configuration.Configuracao,
                    DestinosId = input.DestinosId
                });
            }

            using var transaction = await _dataContext.Database.BeginTransactionAsync();
            try
            {
                _dataContext.ContasEnvio.AddRange(contasEnvio);
                var result = await _dataContext.SaveChangesAsync();

                if (result > 0)
                {
                    await transaction.CommitAsync();
                    _logger.LogInformation("Shipping accounts added and transaction committed successfully.");
                    return true;
                }
                else
                {
                    await transaction.RollbackAsync();
                    _logger.LogWarning("No changes saved to the database. Transaction rolled back.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "An error occurred while adding shipping accounts. Transaction rolled back.");
                return false;
            }
        }

        public async Task<bool> DeleteAccounts(int destinosId)
        {
            _logger.LogInformation("Deleting existing shipping accounts for DestinosId {DestinosId}.", destinosId);

            using var transaction = await _dataContext.Database.BeginTransactionAsync();
            try
            {
                var contasEnvio = await _dataContext.ContasEnvio
                    .Where(c => c.DestinosId == destinosId)
                    .ToListAsync();

                if (contasEnvio == null || contasEnvio.Count == 0)
                {
                    _logger.LogInformation("No existing shipping accounts found for DestinosId {DestinosId}.", destinosId);
                    return true;
                }

                _dataContext.ContasEnvio.RemoveRange(contasEnvio);
                var result = await _dataContext.SaveChangesAsync();

                if (result > 0)
                {
                    await transaction.CommitAsync();
                    _logger.LogInformation("Existing shipping accounts deleted successfully for DestinosId {DestinosId}.", destinosId);
                    return true;
                }
                else
                {
                    await transaction.RollbackAsync();
                    _logger.LogWarning("Failed to delete existing shipping accounts. Transaction rolled back.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "An error occurred while deleting shipping accounts. Transaction rolled back.");
                return false;
            }
        }
    }
}