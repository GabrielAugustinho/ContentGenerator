using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.InputPort.EventPort;
using ContentGenerator.Api.Core.UseCases.EventCase.Interfaces;

namespace ContentGenerator.Api.Core.UseCases.EventCase
{
    public class AddEvent : IAddEvent
    {
        private readonly IEventRepository _eventRepository;

        public AddEvent(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<bool> Execute(AddEventInput input)
        {
            var result = await _eventRepository.AddEvent(input);
            return result;
        }
    }
}
