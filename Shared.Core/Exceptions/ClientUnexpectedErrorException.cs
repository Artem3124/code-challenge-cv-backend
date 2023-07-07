using System.Net;

namespace Shared.Core.Exceptions
{
    public class ClientUnexpectedErrorException : Exception
    {
        public HttpStatusCode StatusCode { get; init; }

        public string Content { get; init; }

        public ClientUnexpectedErrorException(string message, HttpStatusCode statusCode, string content) : base(message)
        {
            StatusCode = statusCode;
            Content = content;
        }
    }
}
