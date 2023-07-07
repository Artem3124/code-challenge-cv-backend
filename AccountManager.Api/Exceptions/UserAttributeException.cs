using AccountManager.Contract.Models;

namespace AccountManager.Api.Exceptions
{
    public class UserAttributeException : Exception
    {
        public UserAttribute Attribute { get; init; }

        public string Code { get; set; }

        public UserAttributeException(UserAttribute attribute, string code, string message) : base(message)
        {
            Attribute = attribute;
            Code = code;
        }

        public UserAttributeException(UserAttribute attribute, (string Code, string Message) error) : base(error.Message)
        {
            Attribute = attribute;
            Code = error.Code;
        }
    }
}
