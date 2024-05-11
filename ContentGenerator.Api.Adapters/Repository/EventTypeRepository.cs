using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Core.OutputPort.HomagePort;
using ContentGenerator.Api.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace ContentGenerator.Api.Adapters.Repository
{
    public class EventTypeRepository : IEventTypeRepository
    {
        private readonly DataContext _dataContext;

        public EventTypeRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<SearchEventTypeOutput>?> GetAllEventType()
        {
            List<TipoHomenagem> tipoHomenagems = await _dataContext.TipoHomenagem.ToListAsync();

            return ToListEventTypeOutput(tipoHomenagems);
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
