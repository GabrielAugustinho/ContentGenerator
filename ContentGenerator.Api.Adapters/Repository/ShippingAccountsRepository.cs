using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Core.InputPort.ShippingAccounts;
using ContentGenerator.Api.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace ContentGenerator.Api.Adapters.Repository
{
    public class ShippingAccountsRepository : IShippingAccountsRepository
    {
        private readonly DataContext _dataContext;

        public ShippingAccountsRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> AddAccounts(AddShippingAccountsInput input)
        {
            if (input == null || input.Accounts == null || input.Accounts.Count == 0)
                return false;

            var contasEnvio = new List<ContasEnvio>();

            foreach (var configuration in input.Accounts)
            {
                if (string.IsNullOrWhiteSpace(configuration.Configuracao))
                    return false;


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
                    return true;
                }
                else
                {
                    await transaction.RollbackAsync();
                    return false;
                }
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return false;
            }
        }


        public async Task<bool> DeleteAccounts(int destinosId)
        {
            using var transaction = await _dataContext.Database.BeginTransactionAsync();
            try
            {
                var contasEnvio = await _dataContext.ContasEnvio
                    .Where(c => c.DestinosId == destinosId)
                    .ToListAsync();

                if (contasEnvio == null || contasEnvio.Count == 0)
                    return true; 

                _dataContext.ContasEnvio.RemoveRange(contasEnvio);
                var result = await _dataContext.SaveChangesAsync();

                if (result > 0)
                {
                    await transaction.CommitAsync();
                    return true;
                }
                else
                {
                    await transaction.RollbackAsync();
                    return false;
                }
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

    }
}
