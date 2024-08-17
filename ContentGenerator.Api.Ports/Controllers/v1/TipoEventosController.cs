using ContentGenerator.Api.Core.OutputPort.EventPort;
using ContentGenerator.Api.Core.UseCases.HomenagemCase.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContentGenerator.Api.Ports.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoEventosController : ControllerBase
    {
        private readonly ISearchEventType _searchEventType;
        private readonly ILogger<TipoEventosController> _logger;

        public TipoEventosController(ISearchEventType searchEventType, ILogger<TipoEventosController> logger)
        {
            _searchEventType = searchEventType;
            _logger = logger;
        }

        [HttpGet("v1/GetAll")]
        public async Task<ActionResult<List<SearchEventTypeOutput>>> GetAll()
        {
            _logger.LogInformation("Iniciando a busca por todos os tipos de eventos.");

            try
            {
                var result = await _searchEventType.Execute();
                _logger.LogInformation("Busca por todos os tipos de eventos concluída com sucesso.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar os tipos de eventos.");
                return BadRequest("Ocorreu uma falha ao listar os tipos de eventos.");
            }
        }
    }
}
