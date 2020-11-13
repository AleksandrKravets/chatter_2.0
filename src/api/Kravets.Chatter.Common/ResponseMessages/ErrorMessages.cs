namespace Kravets.Chatter.Common.ResponseMessages
{
    /// <summary>
    /// Represents messages container
    /// </summary>
    public class ErrorMessages
    {
        public const string NoUserWithSuchIdExists = "There is no user with such identifier.";
        public const string WrongIdentifier = "Wrong identifier.";
        public const string RequiredBody = "Required non-empty body.";
        public const string ThereIsNoMessageWithSuchId = "Message with such identifier doesn't exists.";
        public const string MessageDoesntBelongToUser = "Message with such identifier doesn't belong to current current user.";
        public const string SaveMessageError = "An error occurred while saving the message.";
        public const string PasswordValidationError = "Password validation error.";
        public const string NicknameIsAlreadyTaken = "User wih such nickname already exists.";
        public const string MessageToReplyIdIsRequired = "Message to reply identifier is required for reply creation.";
        public const string CantCreateReply = "Can't create reply. Message with such identifier does not exist.";
        public const string UserWithSuchNicknameDoesNotExist = "User with such nickname does not exist";
        public const string PasswordIsIncorrect = "Password is incorrect.";
        public const string RefreshTokenIsNotValid = "Refresh token is not valid.";
        public const string AccessTokenIsNotValid = "Access token is not valid.";
        public const string ClaimNotFound = "Can not find claim.";
        public const string SavedMessageDoesNotBelongToUser = "Saved message does not belong to user.";
        public const string ThereIsNoSavedMessageWithSuchId = "There is no saved message with such identifier.";
        public const string YouCantMuteYouself = "You cant mute youself.";
    }
}
