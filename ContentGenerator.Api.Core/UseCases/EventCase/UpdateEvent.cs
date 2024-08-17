using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.InputPort.EventPort;
using ContentGenerator.Api.Core.UseCases.EventCase.Interfaces;
using Microsoft.Extensions.Logging;

namespace ContentGenerator.Api.Core.UseCases.EventCase
{
    public class UpdateEvent : IUpdateEvent
    {
        private readonly IEventRepository _eventRepository;
        private readonly ILogger<UpdateEvent> _logger;

        public UpdateEvent(IEventRepository eventRepository, ILogger<UpdateEvent> logger)
        {
            _eventRepository = eventRepository;
            _logger = logger;
        }

        public async Task<bool> Execute(UpdateEventInput input)
        {
            _logger.LogInformation("Atualizando evento com ID: {Id}", input.EventoId);

            try
            {
                var result = await _eventRepository.UpdateEvent(input);

                if (result)
                {
                    _logger.LogInformation("Evento com ID {Id} atualizado com sucesso.", input.EventoId);
                }
                else
                {
                    _logger.LogWarning("Falha ao atualizar evento com ID {Id}.", input.EventoId);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar evento com ID {Id}.", input.EventoId);
                return false;
            }
        }
    }
}
