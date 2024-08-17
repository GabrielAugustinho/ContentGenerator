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
        private readonly ILogger<EventoController> _logger;

        public EventoController(ISearchEvent searchEvent, IAddEvent addEvent, IUpdateEvent updateEvent, IDeleteEvent deleteEvent, ILogger<EventoController> logger)
        {
            _searchEvent = searchEvent;
            _addEvent = addEvent;
            _updateEvent = updateEvent;
            _deleteEvent = deleteEvent;
            _logger = logger;
        }

        [HttpGet("v1/GetAllEventsOfMonth")]
        public async Task<ActionResult<List<SearchEventOutput>>> GetAllEventsOfMonth()
        {
            _logger.LogInformation("Obtendo todos os eventos do mês atual.");

            try
            {
                var result = await _searchEvent.EventsOfMonth();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar eventos do mês atual.");
                return BadRequest("Ocorreu uma falha ao listar os eventos deste mês.");
            }
        }

        [HttpPost("v1/GetAllEventsByDate")]
        public async Task<ActionResult<List<SearchEventOutput>>> GetAllEventsByDate(SearchEventInput searchEventInput)
        {
            _logger.LogInformation("Obtendo eventos na data: {Date}", searchEventInput.DateTime);

            try
            {
                var result = await _searchEvent.EventsByDate(searchEventInput);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar eventos por data: {Date}", searchEventInput.DateTime);
                return BadRequest("Ocorreu uma falha ao listar os eventos por data.");
            }
        }

        [HttpPost("v1/Add")]
        public async Task<ActionResult<string>> Add(AddEventInput addEventInput)
        {
            _logger.LogInformation("Adicionando novo evento.");

            try
            {
                var result = await _addEvent.Execute(addEventInput);

                if (!result)
                {
                    _logger.LogWarning("Falha ao adicionar evento.");
                    return BadRequest("Ocorreu um erro ao tentar adicionar o evento.");
                }

                _logger.LogInformation("Evento adicionado com sucesso.");
                return Ok("Evento adicionado com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar evento.");
                return BadRequest("Ocorreu uma falha ao adicionar o evento.");
            }
        }

        [HttpPut("v1/Update")]
        public async Task<ActionResult<string>> Update(UpdateEventInput input)
        {
            _logger.LogInformation("Atualizando evento com ID: {Id}", input.EventoId);

            try
            {
                var result = await _updateEvent.Execute(input);

                if (!result)
                {
                    _logger.LogWarning("Falha ao atualizar evento com ID: {Id}", input.EventoId);
                    return NotFound("Ocorreu um erro ao tentar atualizar o evento, verifique se o Id é válido.");
                }

                _logger.LogInformation("Evento com ID {Id} atualizado com sucesso.", input.EventoId);
                return Ok("Evento atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar evento com ID: {Id}", input.EventoId);
                return BadRequest("Ocorreu uma falha ao atualizar o evento.");
            }
        }

        [HttpDelete("v1/Delete/{id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            _logger.LogInformation("Deletando evento com ID: {Id}", id);

            try
            {
                var result = await _deleteEvent.Execute(id);

                if (!result)
                {
                    _logger.LogWarning("Falha ao deletar evento com ID: {Id}", id);
                    return NotFound("Ocorreu um erro ao tentar deletar o evento, verifique se o Id é válido.");
                }

                _logger.LogInformation("Evento com ID {Id} deletado com sucesso.", id);
                return Ok("Evento deletado com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar evento com ID: {Id}", id);
                return BadRequest("Ocorreu uma falha ao deletar o evento.");
            }
        }
    }
}