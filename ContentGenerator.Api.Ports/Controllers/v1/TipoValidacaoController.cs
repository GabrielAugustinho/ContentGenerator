using ContentGenerator.Api.Core.OutputPort.ValidationPort;
using ContentGenerator.Api.Core.UseCases.ValidationCase;
using Microsoft.AspNetCore.Mvc;

namespace ContentGenerator.Api.Ports.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoValidacaoController : ControllerBase
    {
        private readonly ISearchValidation _searchValidation;

        public TipoValidacaoController(ISearchValidation searchValidation)
        {
            _searchValidation = searchValidation;
        }

        [HttpGet("v1/GetAll")]
        public async Task<ActionResult<List<SearchValidationOutput>>> GetAll()
        {
            try
            {
                var result = await _searchValidation.Execute();
                return Ok(result);
            }
            catch
            {
                return BadRequest("Ocorreu uma falha ao listar os tipos de validação.");
            }
        }
    }
}
