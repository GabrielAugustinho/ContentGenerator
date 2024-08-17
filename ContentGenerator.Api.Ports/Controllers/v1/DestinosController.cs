using ContentGenerator.Api.Core.OutputPort.DestinyPort;
using ContentGenerator.Api.Core.UseCases.DestinyCase;
using Microsoft.AspNetCore.Mvc;

namespace ContentGenerator.Api.Ports.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinosController : ControllerBase
    {
        private readonly ISearchDestiny _searchDestiny;
        private readonly ILogger<DestinosController> _logger;

        public DestinosController(ISearchDestiny searchDestiny, ILogger<DestinosController> logger)
        {
            _searchDestiny = searchDestiny;
            _logger = logger;
        }

        [HttpGet("v1/GetAll")]
        public async Task<ActionResult<IEnumerable<SearchDestinyOutput>?>> GetAll()
        {
            _logger.LogInformation("Iniciando a busca de todos os destinos.");

            try
            {
                var result = await _searchDestiny.Execute();
                _logger.LogInformation("Busca concluída com sucesso.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu uma falha ao listar os destinos.");
                return BadRequest("Ocorreu uma falha ao listar os destinos.");
            }
        }
    }
}
