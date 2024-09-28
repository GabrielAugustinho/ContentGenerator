namespace ContentGenerator.Api.Core.OutputPort.ContentPort
{
    public class ContentGeneratorResponse(int contentId, string content, string dalleResponse)
    {
        public int ContentId { get; set; } = contentId;
        public string ContentGenerated { get; set; } = content;
        public string ImageResponse { get; set; } = dalleResponse;
    }
}
