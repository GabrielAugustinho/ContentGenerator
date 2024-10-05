namespace ContentGenerator.Api.Core.InputPort.ContentPort
{
    public class UpdateContentInput
    {
        public int ContentId { get; set; } 
        public int ValidationId { get; set; }
        public string? PostValidated { get; set; }
        public string? PostImage { get; set; }
        public bool UserImage { get; set; }
        public bool Active { get; set; }
    }
}
