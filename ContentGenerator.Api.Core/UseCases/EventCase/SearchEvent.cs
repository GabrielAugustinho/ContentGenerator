using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.InputPort.EventPort;
using ContentGenerator.Api.Core.OutputPort.EventPort;
using ContentGenerator.Api.Core.UseCases.EventCase.Interfaces;
using Microsoft.Extensions.Logging;

namespace ContentGenerator.Api.Core.UseCases.EventCase
{
    public class SearchEvent : ISearchEvent
    {
        private readonly IEventRepository _eventRepository;
        private readonly ILogger<SearchEvent> _logger;

        public SearchEvent(IEventRepository eventRepository, ILogger<SearchEvent> logger)
        {
            _eventRepository = eventRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<SearchEventOutput>?> EventsByDate(SearchEventInput inputPort)
        {
            _logger.LogInformation("Buscando eventos na data: {Date}", inputPort.DateTime);

            try
            {
                var outputList = await _eventRepository.GetEventsByDate(inputPort);
                _logger.LogInformation("Busca por eventos na data {Date} concluída com sucesso.", inputPort.DateTime);
                return outputList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar eventos na data: {Date}", inputPort.DateTime);
                return null;
            }
        }

        public async Task<IEnumerable<SearchEventOutput>?> EventsOfMonth()
        {
            _logger.LogInformation("Buscando eventos do mês atual.");

            try
            {
                var outputList = await _eventRepository.GetEventsOfMonth();
                _logger.LogInformation("Busca por eventos do mês atual concluída com sucesso.");
                return outputList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar eventos do mês atual.");
                return null;
            }
        }
    }
}