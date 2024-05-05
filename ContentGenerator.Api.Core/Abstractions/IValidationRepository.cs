using ContentGenerator.Api.Core.OutputPort.ValidationPort;

namespace ContentGenerator.Api.Core.Abstractions
{
    public interface IValidationRepository
    {
        Task<IEnumerable<SearchValidationOutput>?> GetAllValidationType();
    }
}
