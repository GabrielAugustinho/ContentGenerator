using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.InputPort.EventPort;
using ContentGenerator.Api.Core.OutputPort.EventPort;
using ContentGenerator.Api.Core.UseCases.EventCase.Interfaces;

namespace ContentGenerator.Api.Core.UseCases.EventCase
{
    public class SearchEvent : ISearchEvent
    {
        private readonly IEventRepository _eventRepository;

        public SearchEvent(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<IEnumerable<SearchEventOutput>?> EventsByDate(SearchEventInput inputPort)
        {
            var outputList = await _eventRepository.GetEventsByDate(inputPort);

            return outputList;
        }

        public async Task<IEnumerable<SearchEventOutput>?> EventsOfMonth()
        {
            var outputList = await _eventRepository.GetEventsOfMonth();

            return outputList;
        }
    }
}
