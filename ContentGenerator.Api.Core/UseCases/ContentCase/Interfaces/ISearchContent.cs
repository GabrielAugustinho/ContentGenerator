using ContentGenerator.Api.Core.OutputPort.ContentPort;

namespace ContentGenerator.Api.Core.UseCases.ContentCase.Interfaces
{
    public interface ISearchContent
    {
        public Task<SearchContentOutput> SearchById(int id);
        public Task<IEnumerable<SearchContentOutput>> ContentsOfMonth();
    }
}
