namespace ContentGenerator.Api.Core.InputPort.EmailPort
{
    public class UpdateEmailInput
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
    }
}
