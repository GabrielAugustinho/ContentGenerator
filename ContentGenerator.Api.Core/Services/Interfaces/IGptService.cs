using ContentGenerator.Api.Core.InputPort.ContentPort;

namespace ContentGenerator.Api.Core.Services.Interfaces
{
    public interface IGptService
    {
        public Task<string> GenerateText(AddContentInput input, string llamaIAKey);
    }
}
