using Kravets.Chatter.BLL.Contracts.Commands.Accounts.Create;
using Kravets.Chatter.BLL.Contracts.Enums;
using Kravets.Chatter.BLL.Contracts.Hashers;
using Kravets.Chatter.BLL.Contracts.Models.Responses;
using Kravets.Chatter.BLL.Contracts.Validators;
using Kravets.Chatter.Common.ResponseMessages;
using Kravets.Chatter.Common.Settings;
using Kravets.Chatter.DAL.Contracts.Queries.Users;
using Kravets.Chatter.DAL.Contracts.Repositories;
using MediatR;
using Microsoft.Extensions.Options;
using OneOf;
using OneOf.Types;
using System.Threading;
using System.Threading.Tasks;

namespace Kravets.Chatter.BLL.Commands.Accounts
{
    /// <summary>
    /// Represents handler to create new user account.
    /// </summary>
    public class CreateAccountRequestHandler : IRequestHandler<CreateAccountRequest, OneOf<Success, BLError>>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordValidator _passwordValidator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly PasswordSettings _passwordSettings;

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="usersRepository">Users repository provider.</param>
        /// <param name="passwordValidator">Password validator provider.</param>
        /// <param name="passwordHasher">Password hasher provider.</param>
        /// <param name="passwordSettings">Password settings provider.</param>
        public CreateAccountRequestHandler(
            IUsersRepository usersRepository, 
            IPasswordValidator passwordValidator,
            IPasswordHasher passwordHasher,
            IOptions<PasswordSettings> passwordSettings) =>
            (_usersRepository, _passwordValidator, _passwordSettings, _passwordHasher) = 
                (usersRepository, passwordValidator, passwordSettings.Value, passwordHasher);

        /// <summary>
        /// Handles request.
        /// </summary>
        /// <param name="request">Request payload.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Unit or business logic error.</returns>
        public async Task<OneOf<Success, BLError>> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
        {
            var passwordValidatioResult = _passwordValidator.Validate(request.Password, _passwordSettings);
            
            if (passwordValidatioResult == PasswordValidationResult.Failure) 
                return new BLError(ErrorMessages.PasswordValidationError);
            
            var user = await _usersRepository.GetAsync(new GetUserByNicknameQuery.Parameters(request.Nickname), cancellationToken);

            if (user.HasValue)
                return new BLError(ErrorMessages.NicknameIsAlreadyTaken);

            var hashedPassword = _passwordHasher.Hash(request.Password);

            await _usersRepository.CreateAsync(new CreateUserQuery.Parameters(request.Nickname, hashedPassword), cancellationToken);

            return new Success();
        }
    }
}
