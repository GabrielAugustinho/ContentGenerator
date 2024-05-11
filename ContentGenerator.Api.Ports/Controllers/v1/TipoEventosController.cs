using ContentGenerator.Api.Core.OutputPort.HomagePort;
using ContentGenerator.Api.Core.UseCases.HomenagemCase.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContentGenerator.Api.Ports.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoEventosController : ControllerBase
    {
        private readonly ISearchEventType _searchValidation;

        public TipoEventosController(ISearchEventType searchEventType)
        {
            _searchValidation = searchEventType;
        }

        [HttpGet("v1/GetAll")]
        public async Task<ActionResult<List<SearchEventTypeOutput>>> GetAll()
        {
            try
            {
                var result = await _searchValidation.Execute();
                return Ok(result);
            }
            catch
            {
                return BadRequest("Ocorreu uma falha ao listar os tipos de eventos.");
            }
        }
    }
}
