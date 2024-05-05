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

        public HumorController(ISearchHumor searchHumor)
        {
            _searchHumor = searchHumor;
        }

        [HttpGet("v1/GetAll")]
        public async Task<ActionResult<IEnumerable<SearchHumorOutput>>> GetAll()
        {
            try
            {
                var result = await _searchHumor.Execute();
                return Ok(result);
            }
            catch
            {
                return BadRequest("Ocorreu uma falha ao listar os humores.");
            }
        }
    }
}
