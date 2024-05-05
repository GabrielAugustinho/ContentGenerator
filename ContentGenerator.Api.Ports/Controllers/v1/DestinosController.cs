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

        public DestinosController(ISearchDestiny searchDestiny)
        {
            _searchDestiny = searchDestiny;
        }

        [HttpGet("v1/GetAll")]
        public async Task<ActionResult<IEnumerable<DestinyOutput>?>> GetAll()
        {
            try
            {
                var result = await _searchDestiny.Execute();
                return Ok(result);
            }
            catch
            {
                return BadRequest("Ocorreu uma falha ao listar os destinos.");
            }
        }
    }
}
