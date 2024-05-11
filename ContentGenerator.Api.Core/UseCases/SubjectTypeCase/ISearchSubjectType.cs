using ContentGenerator.Api.Core.OutputPort.SubjectTypePort;

namespace ContentGenerator.Api.Core.UseCases.SubjectTypeCase
{
    public interface ISearchSubjectType
    {
        Task<IEnumerable<SearchSubjectTypeOutput>?> Execute();
    }
}
