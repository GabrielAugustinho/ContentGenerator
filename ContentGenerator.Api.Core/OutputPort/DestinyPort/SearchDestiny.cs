using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.UseCases.DestinyCase;

namespace ContentGenerator.Api.Core.OutputPort.DestinyPort
{
    public class SearchDestiny : ISearchDestiny
    {
        private readonly IDestinyRepository _destinyRepository;

        public SearchDestiny(IDestinyRepository destinyRepository)
        {
            _destinyRepository = destinyRepository;
        }

        public async Task<IEnumerable<DestinyOutput>?> Execute()
        {
            var outputList = await _destinyRepository.GetAllDestiny();

            return outputList;
        }
    }
}
