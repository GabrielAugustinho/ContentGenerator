using ContentGenerator.Api.Core.OutputPort.DallE;

namespace ContentGenerator.Api.Core.OutputPort.ContentPort
{
    public class ContentGeneratorResponse(int contentId, string content, DalleResponse dalleResponse)
    {
        public int ContentId { get; set; } = contentId;
        public string ContentGenerated { get; set; } = content;
        public DalleResponse ImageResponse { get; set; } = dalleResponse;
    }
}
