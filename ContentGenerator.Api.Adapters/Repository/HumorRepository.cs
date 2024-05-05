using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.Entities;
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

        public async Task<IEnumerable<SearchHumorOutput>?> GetAllHumor()
        {
            List<Humor> humores = await _dataContext.Humor.ToListAsync();

            return ListHumorToListHumorOutput(humores);
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
