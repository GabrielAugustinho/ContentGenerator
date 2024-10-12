using ContentGenerator.Api.Core.InputPort.Models;
using ContentGenerator.Api.Core.InputPort.PublicationPort;
using ContentGenerator.Api.Core.InputPort.WhatsAppPort;
using ContentGenerator.Api.Core.Models;
using ContentGenerator.Api.Core.OutputPort.PostPort;
using ContentGenerator.Api.Core.OutputPort.WhatsAppPort;
using ContentGenerator.Api.Core.UseCases.PublicationCase.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContentGenerator.Api.Ports.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicacaoController : ControllerBase
    {
        private readonly IAddPublication _addPublication;
        private readonly ISearchPostPaged _searchPostPaged;
        private readonly ILogger<PublicacaoController> _logger;
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _twillioNumber;
        private readonly string _imgBBApiKey;

        public PublicacaoController(IAddPublication addPublication, 
                                    ILogger<PublicacaoController> logger,
                                    IConfiguration configuration,
                                    ISearchPostPaged searchPostPaged)
        {
            _addPublication = addPublication;
            _logger = logger;

            _accountSid = configuration["TWILIO_ACCOUNT_SID"];
            _authToken = configuration["TWILIO_AUTH_TOKEN"];
            _twillioNumber = configuration["TWILIO_SEND_NUMBER"];
            _imgBBApiKey = configuration["IMGBB_API_KEY"];
            _searchPostPaged = searchPostPaged;
        }

        [HttpPost("v1/Add")]
        public async Task<ActionResult<string>> Add(PublicationInput input)
        {
            try
            {
                var tokens = new TwillioKeysModel(_accountSid, _authToken, _twillioNumber, _imgBBApiKey);
                var result = await _addPublication.Execute(input, tokens);

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

        [HttpPost("v1/GetAll")]
        public async Task<ActionResult<PageModel<SearchPostOutput>>> GetAll(PaginationInput input)
        {
            _logger.LogInformation("Iniciando a busca de todas a publicacoes.");

            try
            {
                input.Active = null;
                var result = await _searchPostPaged.SearchPaged(input);
                _logger.LogInformation("Busca de todas as publicacoes.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar todas publicacoes.");
                return BadRequest("Ocorreu uma falha ao listar as publicacoes.");
            }
        }

        [HttpPost("v1/GetActives")]
        public async Task<ActionResult<PageModel<SearchPostOutput>>> GetActives(PaginationInput input)
        {
            _logger.LogInformation("Iniciando a busca das publicacoes ativas.");

            try
            {
                input.Active = true;
                var result = await _searchPostPaged.SearchPaged(input);
                _logger.LogInformation("Busca de publicacoes ativas concluída com sucesso.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar publicacoes ativas.");
                return BadRequest("Ocorreu uma falha ao listar as publicacoes.");
            }
        }

        [HttpPost("v1/GetInactives")]
        public async Task<ActionResult<PageModel<SearchPostOutput>>> GetInactives(PaginationInput input)
        {
            _logger.LogInformation("Iniciando a busca das publicacoes.");

            try
            {
                input.Active = false;
                var result = await _searchPostPaged.SearchPaged(input);
                _logger.LogInformation("Busca de publicacoes inativas concluída com sucesso.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar publicacoes inativas.");
                return BadRequest("Ocorreu uma falha ao listar as publicacoess inativas.");
            }
        }
    }
}
