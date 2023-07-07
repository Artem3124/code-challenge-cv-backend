using AccountManager.Contract.Models;
using Shared.Core.Constants;

namespace AccountManager.Api.Exceptions
{
    public class UserAlreadyExistsException : UserAttributeException
    {
        public UserAlreadyExistsException(string email, UserAttribute attribute)
            : base(attribute, Errors.UserExists.Code, $"User with email address {email} already exists.")
        {

        }
    }
}
