using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Core.OutputPort.EventPort;
using ContentGenerator.Api.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ContentGenerator.Api.Adapters.Repository
{
    public class EventTypeRepository : IEventTypeRepository
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<EventTypeRepository> _logger;

        public EventTypeRepository(DataContext dataContext, ILogger<EventTypeRepository> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        public async Task<IEnumerable<SearchEventTypeOutput>?> GetAllEventType()
        {
            _logger.LogInformation("Buscando todos os tipos de eventos.");

            try
            {
                List<TipoHomenagem> tipoHomenagems = await _dataContext.TipoHomenagem.ToListAsync();
                _logger.LogInformation("Busca por todos os tipos de eventos concluída com sucesso.");
                return ToListEventTypeOutput(tipoHomenagems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todos os tipos de eventos.");
                return null;
            }
        }

        private static List<SearchEventTypeOutput> ToListEventTypeOutput(List<TipoHomenagem> tipoHomenagems)
        {
            var output = new List<SearchEventTypeOutput>();

            foreach (var item in tipoHomenagems)
                output.Add(new SearchEventTypeOutput(item.TipoHomenagemId, item.Descricao));

            return output;
        }
    }
}
