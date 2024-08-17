using ContentGenerator.Api.Core.OutputPort.SubjectTypePort;
using ContentGenerator.Api.Core.UseCases.SubjectTypeCase;
using Microsoft.AspNetCore.Mvc;

namespace ContentGenerator.Api.Ports.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoAssuntoController : ControllerBase
    {
        private readonly ISearchSubjectType _searchSubjectType;
        private readonly ILogger<TipoAssuntoController> _logger;

        public TipoAssuntoController(ISearchSubjectType searchSubjectType, ILogger<TipoAssuntoController> logger)
        {
            _searchSubjectType = searchSubjectType;
            _logger = logger;
        }

        [HttpGet("v1/GetAll")]
        public async Task<ActionResult<List<SearchSubjectTypeOutput>>> GetAll()
        {
            _logger.LogInformation("Iniciando a busca por todos os tipos de assunto.");

            try
            {
                var result = await _searchSubjectType.Execute();
                _logger.LogInformation("Busca por todos os tipos de assunto concluída com sucesso.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar os tipos de assunto.");
                return BadRequest("Ocorreu uma falha ao listar os tipos de assunto.");
            }
        }
    }
}
