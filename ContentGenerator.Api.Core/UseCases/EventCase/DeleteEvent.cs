using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.UseCases.EventCase.Interfaces;

namespace ContentGenerator.Api.Core.UseCases.EventCase
{
    public class DeleteEvent : IDeleteEvent
    {
        private readonly IEventRepository _eventRepository;

        public DeleteEvent(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<bool> Execute(int id)
        {
            var result = await _eventRepository.DeleteEvent(id);
            return result;
        }
    }
}
