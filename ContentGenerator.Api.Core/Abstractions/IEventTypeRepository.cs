using ContentGenerator.Api.Core.OutputPort.HomagePort;

namespace ContentGenerator.Api.Core.Abstractions
{
    public interface IEventTypeRepository
    {
        Task<IEnumerable<SearchEventTypeOutput>?> GetAllEventType();
    }
}
