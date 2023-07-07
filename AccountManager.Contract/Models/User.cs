using AccountManager.Data.Enum;

namespace AccountManager.Contract.Models
{
    public class User
    {
        public Guid UUID { get; set; }

        public Role Role { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public SubscriptionType SubscriptionType { get; set; }
#nullable disable
        public User()
        {

        }
        public User(Guid uuid, Role role, string email, string login, SubscriptionType subscriptionType)
        {
            UUID = uuid;
            Role = role;
            Email = email;
            Login = login;
            SubscriptionType = subscriptionType;
        }
    }
}
