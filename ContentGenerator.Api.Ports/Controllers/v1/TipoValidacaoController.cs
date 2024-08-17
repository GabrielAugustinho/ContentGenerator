using ContentGenerator.Api.Core.OutputPort.ValidationPort;
using ContentGenerator.Api.Core.UseCases.ValidationCase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ContentGenerator.Api.Ports.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoValidacaoController : ControllerBase
    {
        private readonly ISearchValidation _searchValidation;
        private readonly ILogger<TipoValidacaoController> _logger;

        public TipoValidacaoController(ISearchValidation searchValidation, ILogger<TipoValidacaoController> logger)
        {
            _searchValidation = searchValidation;
            _logger = logger;
        }

        [HttpGet("v1/GetAll")]
        public async Task<ActionResult<List<SearchValidationOutput>>> GetAll()
        {
            _logger.LogInformation("Iniciando a busca por todos os tipos de validação.");

            try
            {
                var result = await _searchValidation.Execute();
                _logger.LogInformation("Busca por todos os tipos de validação concluída com sucesso.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar os tipos de validação.");
                return BadRequest("Ocorreu uma falha ao listar os tipos de validação.");
            }
        }
    }
}
