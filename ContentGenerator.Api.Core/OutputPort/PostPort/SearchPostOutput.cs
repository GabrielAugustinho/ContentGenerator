namespace ContentGenerator.Api.Core.OutputPort.PostPort
{
    public class SearchPostOutput(int postId, 
                                  int contentId,
                                  string? validationType, 
                                  string? humor, 
                                  string? destiny, 
                                  string? contentType,
                                  DateTime? criationDate,
                                  string? userDesc,
                                  DateTime? generationDate,
                                  string? originalPost,
                                  DateTime? validationDate,
                                  string? validatedPost,
                                  DateTime? postDate,
                                  string? postImage,
                                  bool? userImage,
                                  bool? active,
                                  int? totalCount)
    {
        public int PostId { get; set; } = postId;
        public int ContentId { get; set; } = contentId;
        public string? ValidationType { get; set; } = validationType;
        public string? Humor { get; set; } = humor;
        public string? Destiny { get; set; } = destiny;
        public string? ContentType { get; set; } = contentType;
        public DateTime? CreationDate { get; set; } = criationDate;
        public string? UserDesc { get; set; } = userDesc;
        public DateTime? GenerationDate { get; set; } = generationDate;
        public string? OriginalPost { get; set; } = originalPost;
        public DateTime? ValidationDate { get; set; } = validationDate;
        public string? ValidatedPot { get; set; } = validatedPost;
        public DateTime? PostDate { get; set; } = postDate;
        public string? PostImage { get; set; } = postImage;
        public bool? UserImage { get; set; } = userImage;
        public bool? Active { get; set; } = active; 
        public int? TotalCount { get; set; } = totalCount;
    }
}
