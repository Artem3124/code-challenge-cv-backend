using Gateway.Contact.Models;

namespace Gateway.Api.Interfaces
{
    public interface ICodeProblemProvider
    {
        Task<Guid> CreateAsync(CreateCodeProblemRequest request);

        Task<List<CodeProblem>> GetForUser(Guid userUUID);

        Task<CodeProblem> Get(Guid uuid);
    }
}