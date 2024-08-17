using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.OutputPort.DestinyPort;
using Microsoft.Extensions.Logging;

namespace ContentGenerator.Api.Core.UseCases.DestinyCase
{
    public class SearchDestiny : ISearchDestiny
    {
        private readonly IDestinyRepository _destinyRepository;
        private readonly ILogger<SearchDestiny> _logger;

        public SearchDestiny(IDestinyRepository destinyRepository, ILogger<SearchDestiny> logger)
        {
            _destinyRepository = destinyRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<SearchDestinyOutput>?> Execute()
        {
            _logger.LogInformation("Executando o caso de uso de busca de destinos.");

            var outputList = await _destinyRepository.GetAllDestiny();

            if (outputList == null || !outputList.Any())
            {
                _logger.LogWarning("Nenhum destino foi encontrado.");
            }
            else
            {
                _logger.LogInformation($"{outputList.Count()} destinos encontrados.");
            }

            return outputList;
        }
    }
}
