using AccountManager.Contract.Models;
using AccountManager.Data.Enum;
using Shared.Core.Clients;

namespace AccountManager.Contract
{
    public interface IAccountManagerClient
    {
        Task<SubscriptionType> GetSubscriptionByUserUUIDAsync(Guid userUUID);

        Task<User> LoginAsync(LoginRequest request);

        Task<User> RegisterAsync(UserCreateRequest request);

        Task<ValidationResult> UpdateUserAsync(Guid uuid, UserUpdateRequest request);

        Task<List<User>> Get();

        Task<User> Get(Guid uuid);
    }

    public class AccountManagerClient : HttpClientBase, IAccountManagerClient
    {
        public AccountManagerClient(string baseUrl) : base(baseUrl)
        {

        }

        public Task<SubscriptionType> GetSubscriptionByUserUUIDAsync(Guid userUUID)
        {
            return Get<SubscriptionType>($"api/Subscription/{userUUID}");
        }

        public Task<User> LoginAsync(LoginRequest request)
        {
            return Post<User, LoginRequest>("api/Authorization/login", request);
        }

        public Task<User> RegisterAsync(UserCreateRequest request)
        {
            return Post<User, UserCreateRequest>("api/Authorization/register", request);
        }

        public Task<ValidationResult> UpdateUserAsync(Guid uuid, UserUpdateRequest request)
        {
            return Put<ValidationResult, UserUpdateRequest>($"api/User/{uuid}", request);
        }

        public Task<User> Get(Guid uuid)
        {
            return Get<User>($"api/User/{uuid}");
        }

        public Task<List<User>> Get()
        {
            return Get<List<User>>("api/User");
        }
    }
}