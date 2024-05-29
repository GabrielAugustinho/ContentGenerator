using ContentGenerator.Api.Core.InputPort.EventPort;
using ContentGenerator.Api.Core.OutputPort.EventPort;
using ContentGenerator.Api.Core.UseCases.EventCase.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContentGenerator.Api.Ports.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly ISearchEvent _searchEvent;

        public EventoController(ISearchEvent searchEvent)
        {
            _searchEvent = searchEvent;
        }

        [HttpGet("v1/GetAllEventsOfMonth")]
        public async Task<ActionResult<List<SearchEventOutput>>> GetAllEventsOfMonth()
        {
            try
            {
                var result = await _searchEvent.EventsOfMonth();
                return Ok(result);
            }
            catch
            {
                return BadRequest("Ocorreu uma falha ao listar os eventos deste mês.");
            }
        }

        [HttpPost("v1/GetAllEventsByDate")]
        public async Task<ActionResult<List<SearchEventOutput>>> GetAllEventsByDate(SearchEventInput searchEventInput)
        {
            try
            {
                var result = await _searchEvent.EventsByDate(searchEventInput);
                return Ok(result);
            }
            catch
            {
                return BadRequest("Ocorreu uma falha ao listar os eventos deste mês.");
            }
        }
    }
}
