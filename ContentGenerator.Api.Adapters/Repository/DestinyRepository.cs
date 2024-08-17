using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Core.OutputPort.DestinyPort;
using ContentGenerator.Api.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ContentGenerator.Api.Adapters.Repository
{
    public class DestinyRepository : IDestinyRepository
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<DestinyRepository> _logger;

        public DestinyRepository(DataContext dataContext, ILogger<DestinyRepository> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        public async Task<IEnumerable<SearchDestinyOutput>> GetAllDestiny()
        {
            _logger.LogInformation("Iniciando a busca de todos os destinos no banco de dados.");

            try
            {
                List<Destinos> destinos = await _dataContext.Destinos.ToListAsync();
                _logger.LogInformation($"{destinos.Count} destinos recuperados do banco de dados.");

                return ListDestinosToListDestinyOutput(destinos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar destinos no banco de dados.");
                throw;
            }
        }

        private static List<SearchDestinyOutput> ListDestinosToListDestinyOutput(List<Destinos> destinos)
        {
            var output = new List<SearchDestinyOutput>();

            foreach (var item in destinos)
            {
                output.Add(new SearchDestinyOutput(item.DestinosId, item.Descricao));
            }

            return output;
        }
    }
}
