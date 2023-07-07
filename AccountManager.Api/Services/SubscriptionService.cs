using AccountManager.Api.Interfaces;
using AccountManager.Data;
using AccountManager.Data.Enum;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;

namespace AccountManager.Api.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly AccountManagerContext _db;

        public SubscriptionService(AccountManagerContext db)
        {
            _db = db.ThrowIfNull();
        }

        public async Task<SubscriptionType> GetByUserUUIDAsync(Guid userUUID, CancellationToken cancellationToken = default)
        {
            var user = await _db.Users
                .Where(u => u.UUID == userUUID)
                .FirstOrDefaultAsync(cancellationToken);
            if (user == default)
            {
                throw new ObjectNotFoundException(userUUID, "user");
            }

            return user.SubscriptionType;
        }
    }
}
