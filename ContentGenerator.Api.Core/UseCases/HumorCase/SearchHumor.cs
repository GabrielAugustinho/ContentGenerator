using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.OutputPort.HumorPort;

namespace ContentGenerator.Api.Core.UseCases.HumorCase
{
    public class SearchHumor : ISearchHumor
    {
        private readonly IHumorRepository _humorRepository;

        public SearchHumor(IHumorRepository humorRepository)
        {
            _humorRepository = humorRepository;
        }

        public async Task<IEnumerable<HumorOutput>?> Execute()
        {
            var outputList = await _humorRepository.GetAllHumor();

            return outputList;
        }
    }
}
