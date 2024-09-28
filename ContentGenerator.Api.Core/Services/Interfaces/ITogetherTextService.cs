using ContentGenerator.Api.Core.InputPort.ContentPort;

namespace ContentGenerator.Api.Core.Services.Interfaces
{
    public interface ITogetherTextService
    {
        Task<string> GenerateText(AddContentInput input, string togetherApiKey);
    }
}
