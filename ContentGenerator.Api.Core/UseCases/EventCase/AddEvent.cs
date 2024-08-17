using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.InputPort.EventPort;
using ContentGenerator.Api.Core.UseCases.EventCase.Interfaces;
using Microsoft.Extensions.Logging;

namespace ContentGenerator.Api.Core.UseCases.EventCase
{
    public class AddEvent : IAddEvent
    {
        private readonly IEventRepository _eventRepository;
        private readonly ILogger<AddEvent> _logger;

        public AddEvent(IEventRepository eventRepository, ILogger<AddEvent> logger)
        {
            _eventRepository = eventRepository;
            _logger = logger;
        }

        public async Task<bool> Execute(AddEventInput input)
        {
            _logger.LogInformation("Adicionando novo evento.");

            try
            {
                var result = await _eventRepository.AddEvent(input);

                if (result)
                {
                    _logger.LogInformation("Evento adicionado com sucesso.");
                }
                else
                {
                    _logger.LogWarning("Falha ao adicionar evento.");
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar evento.");
                return false;
            }
        }
    }
}