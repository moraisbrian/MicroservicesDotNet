namespace Auth.API.Configuration
{
    public class AuthSettings
    {
        public string Secret { get; set; }
        public int ExpiresMinute { get; set; }
        public string ValidAudiences { get; set; }
        public string IdentityUrl { get; set; }
        public string IdentityApiKey { get; set; }
    }
}
