using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.OutputPort.HumorPort;
using Microsoft.Extensions.Logging;

namespace ContentGenerator.Api.Core.UseCases.HumorCase
{
    public class SearchHumor : ISearchHumor
    {
        private readonly IHumorRepository _humorRepository;
        private readonly ILogger<SearchHumor> _logger;

        public SearchHumor(IHumorRepository humorRepository, ILogger<SearchHumor> logger)
        {
            _humorRepository = humorRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<SearchHumorOutput>?> Execute()
        {
            _logger.LogInformation("Iniciando a busca por todos os humores.");

            try
            {
                var outputList = await _humorRepository.GetAllHumor();
                _logger.LogInformation("Busca por todos os humores concluída com sucesso.");
                return outputList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todos os humores.");
                return null;
            }
        }
    }
}
