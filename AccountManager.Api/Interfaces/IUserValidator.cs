using AccountManager.Contract.Models;

namespace AccountManager.Api.Interfaces
{
    public interface IUserValidator
    {
        void ValidateCreateUserRequest(UserCreateRequest request);

        void ValidateLogin(string value);

        void ValidateEmail(string value);

        void ValidatePassword(string value, string valueRepeat);
    }
}