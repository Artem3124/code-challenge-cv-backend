using AccountManager.Contract.Models;

namespace AccountManager.Api.Interfaces
{
    public interface IUserService
    {
        Task<User> GetAsync(Guid uuid);

        Task<List<User>> GetAsync();

        Task<int> UpdateAsync(Guid uuid, UserUpdateRequest request);

        Task<User> CreateAsync(UserCreateRequest request);
    }
}