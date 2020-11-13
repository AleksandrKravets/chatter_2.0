using Kravets.Chatter.BLL.Contracts.Enums;
using Kravets.Chatter.Common.Settings;

namespace Kravets.Chatter.BLL.Contracts.Validators
{
    /// <summary>
    /// Represents password validator.
    /// </summary>
    public interface IPasswordValidator
    {
        PasswordValidationResult Validate(string password, PasswordSettings settings);
    }
}
