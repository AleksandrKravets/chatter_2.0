namespace Kravets.Chatter.BLL.Contracts.Models.Responses
{
    /// <summary>
    /// Represents business logic error model.
    /// </summary>
    public class BLError
    {
        public string Message { get; private set; }

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="message">Error message.</param>
        public BLError(string message)
        {
            Message = message;
        }
    }
}
