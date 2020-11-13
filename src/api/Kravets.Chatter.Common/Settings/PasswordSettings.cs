namespace Kravets.Chatter.Common.Settings
{
    public class PasswordSettings
    {
        public bool RequireDigit { get; set; }
        public int RequiredLength { get; set; }
        public bool RequireLowercase { get; set; }
        public bool RequireUppercase { get; set; }
        public bool RequiredUniqueChars { get; set; }
    }
}
