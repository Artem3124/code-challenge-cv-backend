using AccountManager.Data.Models;
using AccountContract = AccountManager.Contract.Models;

namespace AccountManager.Api.Interfaces
{
    public interface IAuthorizationService
    {
        Task<User?> LoginAsync(string email, string password);
        Task<User> RegisterAsync(AccountContract.UserCreateRequest request);
    }
}