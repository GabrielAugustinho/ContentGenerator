namespace ContentGenerator.Api.Core.Models
{
    public class KeysModel
    {
        public readonly string OpenIAKey = string.Empty;
        public readonly string LlamaIAKey = string.Empty;
        public readonly string TogetherApiKey = string.Empty;

        public KeysModel(string openIAKey, string llamaIAKey, string togetherApiKey)
        {
            this.OpenIAKey = openIAKey;
            this.LlamaIAKey = llamaIAKey;
            this.TogetherApiKey = togetherApiKey;
        }
    }
}
