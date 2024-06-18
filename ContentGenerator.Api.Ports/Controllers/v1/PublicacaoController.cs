using ContentGenerator.Api.Core.InputPort.PublicationPort;
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

        public PublicacaoController(IAddPublication addPublication, ILogger<PublicacaoController> logger)
        {
            _addPublication = addPublication;
            _logger = logger;
        }

        [HttpPost("v1/Add")]
        public async Task<ActionResult<string>> Add(PublicationInput input)
        {
            try
            {
                var result = await _addPublication.Execute(input);

                if (!result)
                {
                    _logger.LogWarning("Falha ao publicar. PublicationInput: {@input}", input);
                    return BadRequest("Ocorreu uma falha ao tentar publicar.");
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
