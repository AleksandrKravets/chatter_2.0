using Kravets.Chatter.BLL.Contracts.Enums;
using Kravets.Chatter.BLL.Contracts.Validators;
using Kravets.Chatter.Common.Settings;
using Microsoft.Extensions.Options;
using System.Linq;

namespace Kravets.Chatter.BLL.Validators
{
    /// <inheritdoc />
    public class PasswordValidator : IPasswordValidator
    {
        private readonly PasswordSettings _passwordSettings;

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="passwordSettings">Password settings.</param>
        public PasswordValidator(IOptions<PasswordSettings> passwordSettings)
        {
            _passwordSettings = passwordSettings.Value;
        }

        /// <inheritdoc />
        public PasswordValidationResult Validate(string password, PasswordSettings settings)
        {
            PasswordHandler handler = new PasswordLengthHandler();

            if (_passwordSettings.RequireDigit) handler = handler.SetNext(new RequireDigitHandler());
            if (_passwordSettings.RequiredUniqueChars)handler = handler.SetNext(new RequiredUniqueCharsHandler());
            if (_passwordSettings.RequireLowercase) handler = handler.SetNext(new RequireLowerCaseHandler());
            if (_passwordSettings.RequireUppercase) handler = handler.SetNext(new RequireUpperCaseHandler());

            return handler.Handle(password, _passwordSettings);
        }
    }

    internal abstract class PasswordHandler
    {
        protected PasswordHandler _nextHandler;

        public PasswordHandler SetNext(PasswordHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public abstract PasswordValidationResult Handle(string password, PasswordSettings settings);
    }

    internal class PasswordLengthHandler : PasswordHandler
    {
        public override PasswordValidationResult Handle(string password, PasswordSettings settings)
        {
            if (password.Length < settings.RequiredLength)
                return PasswordValidationResult.Failure;

            return _nextHandler == null ? PasswordValidationResult.Success : _nextHandler.Handle(password, settings);
        }
    }

    internal class RequireDigitHandler : PasswordHandler
    {
        public override PasswordValidationResult Handle(string password, PasswordSettings settings)
        {
            if (!password.Any(x => char.IsDigit(x)))
                return PasswordValidationResult.Failure;

            return _nextHandler == null ? PasswordValidationResult.Success : _nextHandler.Handle(password, settings);
        }
    }

    internal class RequireLowerCaseHandler : PasswordHandler
    {
        public override PasswordValidationResult Handle(string password, PasswordSettings settings)
        {
            if (!password.Any(x => char.IsLower(x)))
                return PasswordValidationResult.Failure;

            return _nextHandler == null ? PasswordValidationResult.Success : _nextHandler.Handle(password, settings);
        }
    }

    internal class RequireUpperCaseHandler : PasswordHandler
    {
        public override PasswordValidationResult Handle(string password, PasswordSettings settings)
        {
            if (!password.Any(x => char.IsUpper(x)))
                return PasswordValidationResult.Failure;

            return _nextHandler == null ? PasswordValidationResult.Success : _nextHandler.Handle(password, settings);
        }
    }

    internal class RequiredUniqueCharsHandler : PasswordHandler
    {
        public override PasswordValidationResult Handle(string password, PasswordSettings settings)
        {
            if (password.Any(x => password.Count(y => y == x) > 1))
                return PasswordValidationResult.Failure;

            return _nextHandler == null ? PasswordValidationResult.Success : _nextHandler.Handle(password, settings);
        }
    }
}
