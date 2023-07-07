using AccountManager.Data.Enum;
using Shared.Core.Models;

namespace AccountManager.Data.Models
{
    public class User : Entity
    {
        public string Email { get; set; }

        public string Login { get; set; }

        public string PasswordHash { get; set; }

        public SubscriptionType SubscriptionType { get; set; }

        public string Role { get; set; }
    }
}
