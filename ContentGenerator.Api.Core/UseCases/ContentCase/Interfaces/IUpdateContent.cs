using ContentGenerator.Api.Core.InputPort.ContentPort;

namespace ContentGenerator.Api.Core.UseCases.ContentCase.Interfaces
{
    public interface IUpdateContent
    {
        Task<bool> Execute(UpdateContentInput input);
    }
}
