using ContentGenerator.Api.Core.InputPort.ContentPort;
using ContentGenerator.Api.Core.InputPort.WhatsAppPort;
using ContentGenerator.Api.Core.Models;
using ContentGenerator.Api.Core.OutputPort.ContentPort;
using ContentGenerator.Api.Core.OutputPort.WhatsAppPort;
using ContentGenerator.Api.Core.UseCases.ContentCase.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContentGenerator.Api.Ports.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConteudoController : ControllerBase
    {
        private readonly IAddContent _addContent;
        private readonly ILogger<ConteudoController> _logger;
        private readonly string _openAiKey;
        private readonly string _llamaAiKey;

        public ConteudoController(IAddContent addContent,
                                  IConfiguration configuration,
                                  ILogger<ConteudoController> logger)
        {
            _addContent = addContent;
            _logger = logger;

            _openAiKey = configuration["OPENAI_API_KEY"] ?? throw new ArgumentNullException(nameof(configuration), "OpenAI API key is missing.");
            _llamaAiKey = configuration["LLAMA_API_KEY"] ?? throw new ArgumentNullException(nameof(configuration), "Llama API key is missing.");
        }


        [HttpPost("v1/Add")]
        public async Task<ActionResult<ContentGeneratorResponse>> Add(AddContentInput input)
        {
            try
            {
                var result = await _addContent.Execute(input, _openAiKey, _llamaAiKey);

                if (result == null)
                {
                    _logger.LogWarning("Falha ao adicionar o conteudo. Input: {@input}", input);
                    return BadRequest("Ocorreu uma falha ao tentar adicionar o conteúdo.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Uma exception ocorreu durante a adição. Input: {@input}", input);
                return StatusCode(500, "Ocorreu uma exception ao adicionar o conteúdo.");
            }
        }
    }
}
