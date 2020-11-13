namespace Kravets.Chatter.API.Models.Errors
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }

        public ErrorResponse(string message) => (Message) = (message);

        public ErrorResponse() { }

        public bool ShouldSerializeStackTrace() => !string.IsNullOrEmpty(StackTrace);
    }
}
