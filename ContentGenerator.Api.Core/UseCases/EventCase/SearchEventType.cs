using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.OutputPort.EventPort;
using ContentGenerator.Api.Core.UseCases.HomenagemCase.Interfaces;

namespace ContentGenerator.Api.Core.UseCases.HomenagemCase
{
    public class SearchEventType : ISearchEventType
    {
        private readonly IEventTypeRepository _eventTypeRepository;

        public SearchEventType(IEventTypeRepository eventTypeRepository)
        {
            _eventTypeRepository = eventTypeRepository;
        }

        public async Task<IEnumerable<SearchEventTypeOutput>?> Execute()
        {
            var outputList = await _eventTypeRepository.GetAllEventType();

            return outputList;
        }
    }
}
