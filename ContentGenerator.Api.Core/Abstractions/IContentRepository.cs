using ContentGenerator.Api.Core.InputPort.ContentPort;

namespace ContentGenerator.Api.Core.Abstractions
{
    public interface IContentRepository
    {
        Task<bool> AddContent(ContentInput input);
    }
}
