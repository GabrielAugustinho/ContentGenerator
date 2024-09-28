using ContentGenerator.Api.Core.InputPort.ContentPort;

namespace ContentGenerator.Api.Core.Services.Interfaces
{
    public interface ITogetherImageService
    {
        Task<string?> GenerateImage(AddContentInput input, string togetherApiKey);
    }
}
