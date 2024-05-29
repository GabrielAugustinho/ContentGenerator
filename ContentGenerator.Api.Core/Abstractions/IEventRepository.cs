using ContentGenerator.Api.Core.InputPort.EventPort;
using ContentGenerator.Api.Core.OutputPort.EventPort;

namespace ContentGenerator.Api.Core.Abstractions
{
    public interface IEventRepository
    {
        Task<IEnumerable<SearchEventOutput>?> GetEventsOfMonth();
        Task<IEnumerable<SearchEventOutput>?> GetEventsByDate(SearchEventInput inputPort);
        Task<bool> AddEvent(AddEventInput input);
        Task<bool> UpdateEvent(UpdateEventInput input);
        Task<bool> DeleteEvent(int id);
    }
}
