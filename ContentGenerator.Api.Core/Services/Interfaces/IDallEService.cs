using ContentGenerator.Api.Core.InputPort.ContentPort;
using ContentGenerator.Api.Core.OutputPort.DallE;

namespace ContentGenerator.Api.Core.Services.Interfaces
{
    public interface IDallEService
    {
        public Task<DalleResponse> GenerateImage(AddContentInput input, string openIAKey);
    }
}
