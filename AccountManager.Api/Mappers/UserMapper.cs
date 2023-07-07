using DataModels = AccountManager.Data.Models;
using Shared.Core.Interfaces;
using AccountManager.Contract.Models;
using AccountManager.Data.Enum;

namespace AccountManager.Api.Mappers
{
    public interface IUserMapper : IMapper<DataModels.User, User>
    {

    }

    public class UserMapper : IUserMapper
    {
        public User Map(DataModels.User entity) => new (entity.UUID, GetRole(entity.Role), entity.Email, entity.Login, entity.SubscriptionType);

        public List<User> Map(List<DataModels.User> entity) => entity.Select(e => Map(e)).ToList();

        public Role GetRole(string value)
        {
            if (Enum.TryParse(typeof(Role), value, out var role) && role != null)
            {
                return (Role)role;
            }

            return Role.User;
        }
    }
}
