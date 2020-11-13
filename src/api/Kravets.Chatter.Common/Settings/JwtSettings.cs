namespace Kravets.Chatter.Common.Settings
{
    public class JwtSettings
    {
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }
        public int AccessTokenLifetime { get; set; }
        public int RefreshTokenLifetime { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
