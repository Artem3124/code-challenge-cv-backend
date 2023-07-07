using AccountManager.Api.Exceptions;
using AccountManager.Api.Interfaces;
using AccountManager.Contract.Models;
using Shared.Core.Constants;

namespace AccountManager.Api.Services
{
    public class UserValidator : IUserValidator
    {
        public void ValidateCreateUserRequest(UserCreateRequest request)
        {
            ValidateLogin(request.Login);
            ValidateEmail(request.Email);
            ValidatePassword(request.Password, request.RepeatPassword);
        }

        public void ValidateLogin(string value)
        {
            if (!IsValidLogin(value))
            {
                throw new UserAttributeException(UserAttribute.Login, Errors.LoginInvalid);
            }
        }

        public void ValidateEmail(string value)
        {
            if (!IsValidEmail(value))
            {
                throw new UserAttributeException(UserAttribute.Email, Errors.EmailInvalid);
            }
        }

        public void ValidatePassword(string value, string valueRepeat)
        {
            if (!IsValidPassword(value))
            {
                throw new UserAttributeException(UserAttribute.Password, Errors.PasswordLengthInvalid);
            }
            if (value != valueRepeat)
            {
                throw new UserAttributeException(UserAttribute.Password, Errors.PasswordMissmatch);
            }
        }

        private bool IsValidLogin(string value)
        {
            return !(string.IsNullOrEmpty(value) || value.Contains(' ') || value.Contains('@') || value.Contains('\"') || value.Contains('\"'));
        }

        private bool IsValidPassword(string value)
        {
            return value.Length > 7;
        }

        private bool IsValidEmail(string value)
        {
            var trimmedEmail = value.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(value);

                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}
