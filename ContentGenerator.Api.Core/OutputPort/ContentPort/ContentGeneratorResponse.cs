using ContentGenerator.Api.Core.OutputPort.DallE;

namespace ContentGenerator.Api.Core.OutputPort.ContentPort
{
    public class ContentGeneratorResponse(string content, DalleResponse dalleResponse)
    {
        public string ContentGenerated { get; set; } = content;
        public DalleResponse ImageResponse { get; set; } = dalleResponse;
    }
}
