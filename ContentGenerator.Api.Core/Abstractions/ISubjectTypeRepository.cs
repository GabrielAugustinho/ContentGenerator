using ContentGenerator.Api.Core.OutputPort.SubjectTypePort;

namespace ContentGenerator.Api.Core.Abstractions
{
    public interface ISubjectTypeRepository
    {
        Task<IEnumerable<SearchSubjectTypeOutput>?> GetAllSubjectType();

    }
}
