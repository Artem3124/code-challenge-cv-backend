using CodeRunManager.Contract.Models;

namespace CodeRunManager.Api.Interfaces
{
    public interface IStatisticService
    {
        Task<UserStatistic> GetStatisticForUser(Guid userUUID);
    }
}