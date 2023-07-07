using AccountManager.Data.Enum;

namespace AccountManager.Api.Interfaces
{
    public interface ISubscriptionService
    {
        Task<SubscriptionType> GetByUserUUIDAsync(Guid userUUID, CancellationToken cancellationToken = default);
    }
}