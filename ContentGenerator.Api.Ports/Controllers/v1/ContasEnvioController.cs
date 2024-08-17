using ContentGenerator.Api.Core.InputPort.ShippingAccountsPort;
using ContentGenerator.Api.Core.UseCases.ShippingAccountsCase;
using Microsoft.AspNetCore.Mvc;

namespace ContentGenerator.Api.Ports.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContasEnvioController : ControllerBase
    {
        private readonly IAddShippingAccounts _addShippingAccounts;
        private readonly ILogger<ContasEnvioController> _logger;

        public ContasEnvioController(IAddShippingAccounts addShippingAccounts, ILogger<ContasEnvioController> logger)
        {
            _addShippingAccounts = addShippingAccounts;
            _logger = logger;
        }

        [HttpPost("v1/Add")]
        public async Task<ActionResult> Add(AddShippingAccountsInput input)
        {
            _logger.LogInformation("Starting the process to add shipping accounts.");

            try
            {
                var result = await _addShippingAccounts.Execute(input);

                if (!result)
                {
                    _logger.LogWarning("Failed to add shipping accounts.");
                    return BadRequest("Ocorreu um erro ao tentar definir os contatos de envio.");
                }

                _logger.LogInformation("Shipping accounts added successfully.");
                return Ok("Contato para envio adicionado com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding shipping accounts.");
                return BadRequest("Ocorreu uma falha ao definir os contatos de envio.");
            }
        }
    }
}