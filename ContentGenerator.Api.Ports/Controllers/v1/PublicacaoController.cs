using ContentGenerator.Api.Core.InputPort.PublicationPort;
using ContentGenerator.Api.Core.Models;
using ContentGenerator.Api.Core.UseCases.PublicationCase;
using Microsoft.AspNetCore.Mvc;

namespace ContentGenerator.Api.Ports.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicacaoController : ControllerBase
    {
        private readonly IAddPublication _addPublication;
        private readonly ILogger<PublicacaoController> _logger;
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _twillioNumber;

        public PublicacaoController(IAddPublication addPublication, 
                                    ILogger<PublicacaoController> logger,
                                    IConfiguration configuration)
        {
            _addPublication = addPublication;
            _logger = logger;

            _accountSid = configuration["TWILIO_ACCOUNT_SID"];
            _authToken = configuration["TWILIO_AUTH_TOKEN"];
            _twillioNumber = configuration["TWILIO_SEND_NUMBER"];
        }

        [HttpPost("v1/Add")]
        public async Task<ActionResult<string>> Add(PublicationInput input)
        {
            try
            {
                var result = await _addPublication.Execute(input, new TwillioKeysModel(_accountSid, _authToken, _twillioNumber));

                if (!result)
                {
                    _logger.LogWarning("Falha ao publicar. PublicationInput: {@input}", input);
                    return BadRequest("Ocorreu uma falha ao tentar publicar, verifique se o assunto foi validado.");
                }

                return Ok("Conteúdo publicado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Uma exception ocorreu durante publicacao. Input: {@input}", input);
                return StatusCode(500, "Ocorreu uma exception ao publicar o conteúdo.");
            }
        }
    }
}
