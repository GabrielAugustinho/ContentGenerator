using ContentGenerator.Api.Core.OutputPort.DestinyPort;

namespace ContentGenerator.Api.Core.UseCases.DestinyCase
{
    public interface ISearchDestiny
    {
        Task<IEnumerable<SearchDestinyOutput>?> Execute();
    }
}
