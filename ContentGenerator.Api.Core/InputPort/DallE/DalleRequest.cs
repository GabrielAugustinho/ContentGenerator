namespace ContentGenerator.Api.Core.InputPort.DallE
{
    public class DalleRequest
    {
        public string? prompt { get; set; }
        public short? n { get; set; }
        public string? size { get; set; }
    }
}
