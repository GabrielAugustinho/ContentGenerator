namespace ContentGenerator.Api.Core.Models
{
    public class TwillioKeysModel(string accountSid, string authToken, string twillioNumber, string imgBBApiKeys)
    {
        public readonly string AccountSid = accountSid;
        public readonly string AuthToken = authToken;
        public readonly string TwillioNumber = twillioNumber;
        public readonly string ImgBBApiKeys = imgBBApiKeys;
    }
}
