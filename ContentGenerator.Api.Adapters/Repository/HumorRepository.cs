using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Core.OutputPort.DestinyPort;
using ContentGenerator.Api.Core.OutputPort.HumorPort;
using ContentGenerator.Api.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace ContentGenerator.Api.Adapters.Repository
{
    public class HumorRepository : IHumorRepository
    {
        private readonly DataContext _dataContext;

        public HumorRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<HumorOutput>?> GetAllHumor()
        {
            List<Humor> humores = await _dataContext.Humor.ToListAsync();

            return ListHumorToListHumorOutput(humores);
        }

        private static IEnumerable<HumorOutput> ListHumorToListHumorOutput(List<Humor> humores)
        {
            var output = new List<HumorOutput>();

            foreach (var item in humores)
                output.Add(new HumorOutput(item.HumorId, item.Descricao));

            return output;
        }
    }
}
