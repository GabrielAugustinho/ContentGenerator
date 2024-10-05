namespace ContentGenerator.Api.Core.Models
{
    public class IaKeysModel
    {
        public readonly string OpenIAKey = string.Empty;
        public readonly string LlamaIAKey = string.Empty;
        public readonly string TogetherApiKey = string.Empty;

        public IaKeysModel(string openIAKey, string llamaIAKey, string togetherApiKey)
        {
            this.OpenIAKey = openIAKey;
            this.LlamaIAKey = llamaIAKey;
            this.TogetherApiKey = togetherApiKey;
        }
    }
}
