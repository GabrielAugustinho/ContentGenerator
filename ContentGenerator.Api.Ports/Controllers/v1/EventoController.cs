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
        private readonly IUpdateEvent _updateEvent;
        private readonly IDeleteEvent _deleteEvent;

        public EventoController(ISearchEvent searchEvent, IAddEvent addEvent, IUpdateEvent updateEvent, IDeleteEvent deleteEvent)
        {
            _searchEvent = searchEvent;
            _addEvent = addEvent;
            _updateEvent = updateEvent;
            _deleteEvent = deleteEvent;
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

        [HttpPut("v1/Update")]
        public async Task<ActionResult<string>> Update(UpdateEventInput input)
        {
            try
            {
                var result = await _updateEvent.Execute(input);

                if (!result)
                    return NotFound("Ocorreu um erro ao tentar atualizar o evento, verifique se o Id é válido.");

                return Ok("Evento atualizado com sucesso.");
            }
            catch
            {
                return BadRequest("Ocorreu uma falha ao atualizar o evento.");
            }
        }

        [HttpDelete("v1/Delete/{id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                var result = await _deleteEvent.Execute(id);

                if (!result)
                    return NotFound("Ocorreu um erro ao tentar deletar o evento, verifique se o Id é válido.");

                return Ok("Evento deletado com sucesso.");
            }
            catch
            {
                return BadRequest("Ocorreu uma falha ao deletar o evento.");
            }
        }
    }
}
