namespace ContentGenerator.Api.Core.OutputPort.ContentPort
{
    public class SearchContentOutput(int contentId,
                                     string searchValidationOutput,
                                     string searchHumorOutput,
                                     string searchDestinyOutput,
                                     string searchEventTypeOutput,
                                     DateTime criationDate,
                                     string userDesc,
                                     DateTime? generationTextDate,
                                     string? originalPost,
                                     DateTime? validationDate,
                                     string? postValidated,
                                     DateTime? postDate,
                                     string? postImage,
                                     bool userImage,
                                     bool active)
    {
        public int ContentId { get; set; } = contentId;
        public string ValidationType { get; set; } = searchValidationOutput;
        public string Humor { get; set; } = searchHumorOutput;
        public string Destiny { get; set; } = searchDestinyOutput;
        public string EventType { get; set; } = searchEventTypeOutput;
        public DateTime CriationDate { get; set; } = criationDate; // Data da criação do assunto, criei hoje para postar amanhã
        public string UserDesc { get; set; } = userDesc; // o que o usuario inseriu, descrição do que ele quer
        public DateTime? GenerationTextDate { get; set; } = generationTextDate; // Data da geração do texto
        public string? OriginalPost { get; set; } = originalPost; // Post gerado pelo GPT
        public DateTime? ValidationDate { get; set; } = validationDate;
        public string? PostValidated { get; set; } = postValidated;
        public DateTime? PostDate { get; set; } = postDate;
        public string? PostImage { get; set; } = postImage;
        public bool UserImage { get; set; } = userImage;
        public bool Active { get; set; } = active;
    }
}
