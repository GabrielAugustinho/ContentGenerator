using ContentGenerator.Api.Core.OutputPort.HumorPort;
using ContentGenerator.Api.Core.UseCases.HumorCase;
using Microsoft.AspNetCore.Mvc;

namespace ContentGenerator.Api.Ports.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class HumorController : ControllerBase
    {
        private readonly ISearchHumor _searchHumor;
        private readonly ILogger<HumorController> _logger;

        public HumorController(ISearchHumor searchHumor, ILogger<HumorController> logger)
        {
            _searchHumor = searchHumor;
            _logger = logger;
        }

        [HttpGet("v1/GetAll")]
        public async Task<ActionResult<IEnumerable<SearchHumorOutput>>> GetAll()
        {
            _logger.LogInformation("Iniciando a busca por todos os humores.");

            try
            {
                var result = await _searchHumor.Execute();
                _logger.LogInformation("Busca por todos os humores concluída com sucesso.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar todos os humores.");
                return BadRequest("Ocorreu uma falha ao listar os humores.");
            }
        }
    }
}
