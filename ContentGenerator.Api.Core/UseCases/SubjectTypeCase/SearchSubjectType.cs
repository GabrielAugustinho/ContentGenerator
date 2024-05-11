using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.OutputPort.SubjectTypePort;

namespace ContentGenerator.Api.Core.UseCases.SubjectTypeCase
{
    public class SearchSubjectType : ISearchSubjectType
    {
        private readonly ISubjectTypeRepository _subjectTypeRepository;

        public SearchSubjectType(ISubjectTypeRepository subjectTypeRepository)
        {
            _subjectTypeRepository = subjectTypeRepository;
        }

        public async Task<IEnumerable<SearchSubjectTypeOutput>?> Execute()
        {
            var outputList = await _subjectTypeRepository.GetAllSubjectType();

            return outputList;
        }
    }
}
