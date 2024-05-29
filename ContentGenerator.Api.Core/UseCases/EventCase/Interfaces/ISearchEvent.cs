using ContentGenerator.Api.Core.InputPort.EventPort;
using ContentGenerator.Api.Core.OutputPort.EventPort;

namespace ContentGenerator.Api.Core.UseCases.EventCase.Interfaces
{
    public interface ISearchEvent
    {
        Task<IEnumerable<SearchEventOutput>?> EventsOfMonth();
        Task<IEnumerable<SearchEventOutput>?> EventsByDate(SearchEventInput searchEventInput);
    }
}
