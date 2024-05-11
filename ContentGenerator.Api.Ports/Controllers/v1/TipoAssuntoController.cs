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

        public TipoAssuntoController(ISearchSubjectType searchSubjectType)
        {
            _searchSubjectType = searchSubjectType;
        }

        [HttpGet("v1/GetAll")]
        public async Task<ActionResult<List<SearchSubjectTypeOutput>>> GetAll()
        {
            try
            {
                var result = await _searchSubjectType.Execute();
                return Ok(result);
            }
            catch
            {
                return BadRequest("Ocorreu uma falha ao listar os tipos de assunto.");
            }
        }
    }
}
