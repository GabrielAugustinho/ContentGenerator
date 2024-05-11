using ContentGenerator.Api.Core.OutputPort.HomagePort;

namespace ContentGenerator.Api.Core.UseCases.HomenagemCase.Interfaces
{
    public interface ISearchEventType
    {
        Task<IEnumerable<SearchEventTypeOutput>?> Execute();
    }
}
