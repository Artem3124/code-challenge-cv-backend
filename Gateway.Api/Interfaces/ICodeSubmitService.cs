using Gateway.Contact.Models;

namespace Gateway.Api.Interfaces
{
    public interface ICodeSubmitService
    {
        Task<CodeRunProgress> GetProgress(Guid userUUID, Guid runUUID);

        Task<Guid> Submit(CodeSubmitRequest request, Guid userUUID);

        Task<Guid> SubmitChallenge(CodeSubmitRequest request, Guid userUUID);
    }
}