﻿using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.InputPort.ShippingAccountsPort;

namespace ContentGenerator.Api.Core.UseCases.ShippingAccountsCase
{
    public class AddShippingAccounts : IAddShippingAccounts
    {
        private readonly IShippingAccountsRepository _shippingAccountsRepository;

        public AddShippingAccounts(IShippingAccountsRepository shippingAccountsRepository)
        {
            _shippingAccountsRepository = shippingAccountsRepository;
        }

        public async Task<bool> Execute(AddShippingAccountsInput input)
        {
            bool result = await _shippingAccountsRepository.DeleteAccounts(input.DestinosId);
            if (result)
                result = await _shippingAccountsRepository.AddAccounts(input);

            return result;
        }
    }
}
