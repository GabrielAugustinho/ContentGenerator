using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.OutputPort.EventPort;
using ContentGenerator.Api.Core.UseCases.HomenagemCase.Interfaces;
using Microsoft.Extensions.Logging;

namespace ContentGenerator.Api.Core.UseCases.HomenagemCase
{
    public class SearchEventType : ISearchEventType
    {
        private readonly IEventTypeRepository _eventTypeRepository;
        private readonly ILogger<SearchEventType> _logger;

        public SearchEventType(IEventTypeRepository eventTypeRepository, ILogger<SearchEventType> logger)
        {
            _eventTypeRepository = eventTypeRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<SearchEventTypeOutput>?> Execute()
        {
            _logger.LogInformation("Iniciando a busca por todos os tipos de eventos.");

            try
            {
                var outputList = await _eventTypeRepository.GetAllEventType();
                _logger.LogInformation("Busca por todos os tipos de eventos concluída com sucesso.");
                return outputList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todos os tipos de eventos.");
                return null;
            }
        }
    }
}
