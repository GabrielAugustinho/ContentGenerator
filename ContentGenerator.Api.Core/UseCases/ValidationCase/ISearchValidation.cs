using ContentGenerator.Api.Core.OutputPort.ValidationPort;

namespace ContentGenerator.Api.Core.UseCases.ValidationCase
{
    public interface ISearchValidation
    {
        Task<IEnumerable<SearchValidationOutput>?> Execute();
    }
}
