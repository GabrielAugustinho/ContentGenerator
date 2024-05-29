using ContentGenerator.Api.Core.OutputPort.EventPort;

namespace ContentGenerator.Api.Core.Abstractions
{
    public interface IEventTypeRepository
    {
        Task<IEnumerable<SearchEventTypeOutput>?> GetAllEventType();
    }
}
