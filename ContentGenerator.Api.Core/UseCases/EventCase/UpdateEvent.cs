using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.InputPort.EventPort;
using ContentGenerator.Api.Core.UseCases.EventCase.Interfaces;

namespace ContentGenerator.Api.Core.UseCases.EventCase
{
    public class UpdateEvent : IUpdateEvent
    {
        private readonly IEventRepository _eventRepository;

        public UpdateEvent(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<bool> Execute(UpdateEventInput input)
        {
            try
            {
                var result = await _eventRepository.UpdateEvent(input);
                return result;
            }
            catch
            {
                return default!;
            }
        }
    }
}
