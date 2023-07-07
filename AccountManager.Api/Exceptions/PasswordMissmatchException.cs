using AccountManager.Contract.Models;
using Shared.Core.Constants;

namespace AccountManager.Api.Exceptions
{
    public class PasswordMissmatchException : UserAttributeException
    {
        public PasswordMissmatchException() : base(UserAttribute.Password, Errors.PasswordUpdateMismatch)
        {

        }
    }
}
