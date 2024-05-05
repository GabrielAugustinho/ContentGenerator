using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Core.OutputPort.DestinyPort;
using ContentGenerator.Api.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace ContentGenerator.Api.Adapters.Repository
{
    public class DestinyRepository : IDestinyRepository
    {
        private readonly DataContext _dataContext;

        public DestinyRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<SearchDestinyOutput>> GetAllDestiny()
        {
            List<Destinos> destinos = await _dataContext.Destinos.ToListAsync();

            return ListDestinosToListDestinyOutput(destinos);
        }

        private static List<SearchDestinyOutput> ListDestinosToListDestinyOutput(List<Destinos> destinos)
        {
            var output = new List<SearchDestinyOutput>();

            foreach(var item in destinos)
                output.Add(new SearchDestinyOutput(item.DestinosId, item.Descricao));

            return output;
        }
    }
}
