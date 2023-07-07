using AccountManager.Contract.Models;

namespace AccountManager.Api.Exceptions
{
    public class ValidationError
    {
        public string Message { get; set; }

        public string Code { get; set; }

        public UserAttribute UserAttribute { get; set; }

        public ValidationError(string message, string code, UserAttribute userAttribute)
        {
            Message = message;
            Code = code;
            UserAttribute = userAttribute;
        }
    }
}
