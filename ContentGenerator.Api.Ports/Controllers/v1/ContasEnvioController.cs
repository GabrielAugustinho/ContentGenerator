using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Core.InputPort.ShippingAccounts;
using ContentGenerator.Api.Core.UseCases.ShippingAccounts;
using Microsoft.AspNetCore.Mvc;

namespace ContentGenerator.Api.Ports.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContasEnvioController : ControllerBase
    {
        private readonly IAddShippingAccounts _addShippingAccounts;

        public ContasEnvioController(IAddShippingAccounts addShippingAccounts)
        {
            _addShippingAccounts = addShippingAccounts;
        }

        [HttpPost("v1/Add")]
        public async Task<ActionResult<ContasEnvio>> Add(AddShippingAccountsInput input)
        {
            try
            {
                var result = await _addShippingAccounts.Execute(input);

                if (!result)
                    return BadRequest("Ocorreu um erro ao tentar definir os contatos de envio.");

                return Ok("Contato para envio adicionado com sucesso.");
            }
            catch
            {
                return BadRequest("Ocorreu uma falha ao definir os contatod de envio.");
            }
        }
    }
}
