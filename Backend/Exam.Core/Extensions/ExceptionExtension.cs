namespace Exam.Extensions
{
    public class UserFriendlyException : Exception
    {
        public int StatusCode { get; init; } = 400;
        protected string UserFriendlyMessage { get; }

        public UserFriendlyException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
            UserFriendlyMessage = message;
        }

        public UserFriendlyException(int statusCode, string message, Exception innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
            UserFriendlyMessage = message;
        }
        public UserFriendlyException(string message) : base(message)
        {
            UserFriendlyMessage = message;
        }

        public UserFriendlyException(string message, Exception innerException) : base(message, innerException)
        {
            UserFriendlyMessage = message;
        }
    }
}
