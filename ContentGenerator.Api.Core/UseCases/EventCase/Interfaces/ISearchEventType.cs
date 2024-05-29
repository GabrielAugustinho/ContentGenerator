using ContentGenerator.Api.Core.OutputPort.EventPort;

namespace ContentGenerator.Api.Core.UseCases.HomenagemCase.Interfaces
{
    public interface ISearchEventType
    {
        Task<IEnumerable<SearchEventTypeOutput>?> Execute();
    }
}
