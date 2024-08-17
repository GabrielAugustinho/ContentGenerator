using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Core.OutputPort.HumorPort;
using ContentGenerator.Api.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ContentGenerator.Api.Adapters.Repository
{
    public class HumorRepository : IHumorRepository
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<HumorRepository> _logger;

        public HumorRepository(DataContext dataContext, ILogger<HumorRepository> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        public async Task<IEnumerable<SearchHumorOutput>?> GetAllHumor()
        {
            _logger.LogInformation("Buscando todos os humores.");

            try
            {
                List<Humor> humores = await _dataContext.Humor.ToListAsync();
                _logger.LogInformation("Busca por todos os humores concluída com sucesso.");
                return ListHumorToListHumorOutput(humores);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todos os humores.");
                return null;
            }
        }

        private static List<SearchHumorOutput> ListHumorToListHumorOutput(List<Humor> humores)
        {
            var output = new List<SearchHumorOutput>();

            foreach (var item in humores)
                output.Add(new SearchHumorOutput(item.HumorId, item.Descricao));

            return output;
        }
    }
}
