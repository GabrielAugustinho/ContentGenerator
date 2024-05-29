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
        private readonly IAddEvent _addEvent;

        public EventoController(ISearchEvent searchEvent, IAddEvent addEvent)
        {
            _searchEvent = searchEvent;
            _addEvent = addEvent;
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

        [HttpPost("v1/Add")]
        public async Task<ActionResult<string>> Add(AddEventInput addEventInput)
        {
            try
            {
                var result = await _addEvent.Execute(addEventInput);

                if (!result)
                    return BadRequest("Ocorreu um erro ao tentar adicionar o evento.");

                return Ok("Evento adicionado com sucesso.");
            }
            catch
            {
                return BadRequest("Ocorreu uma falha ao adicionar o evento.");
            }
        }
    }
}
