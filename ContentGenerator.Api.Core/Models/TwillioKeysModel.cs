namespace ContentGenerator.Api.Core.Models
{
    public class TwillioKeysModel
    {
        public readonly string AccountSid = string.Empty;
        public readonly string AuthToken = string.Empty;
        public readonly string TwillioNumber = string.Empty;

        public TwillioKeysModel(string accountSid, string authToken, string twillioNumber)
        {
            this.AccountSid = accountSid;
            this.AuthToken = authToken;
            this.TwillioNumber = twillioNumber;
        }
    }
}
