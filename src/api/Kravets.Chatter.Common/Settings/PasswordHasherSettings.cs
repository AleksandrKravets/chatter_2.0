namespace Kravets.Chatter.Common.Settings
{
    public class PasswordHasherSettings
    {
        public int SaltSize { get; set; }
        public int HashSize { get; set; }
        public int Iterations { get; set; }
    }
}
