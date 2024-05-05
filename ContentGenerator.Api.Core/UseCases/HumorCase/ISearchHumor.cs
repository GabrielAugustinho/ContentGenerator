using ContentGenerator.Api.Core.OutputPort.DestinyPort;
using ContentGenerator.Api.Core.OutputPort.HumorPort;

namespace ContentGenerator.Api.Core.UseCases.HumorCase
{
    public interface ISearchHumor
    {
        Task<IEnumerable<HumorOutput>?> Execute();
    }
}
