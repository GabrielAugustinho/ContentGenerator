using ContentGenerator.Api.Core.InputPort.ContentPort;
using ContentGenerator.Api.Core.OutputPort.ContentPort;

namespace ContentGenerator.Api.Core.UseCases.ContentCase.Interfaces
{
    public interface IAddContent
    {
        Task<ContentGeneratorResponse?> Execute(AddContentInput input, string openIAKey, string llamaIAKey);
    }
}
