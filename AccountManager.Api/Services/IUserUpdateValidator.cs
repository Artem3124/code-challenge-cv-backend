namespace AccountManager.Api.Services
{
    public interface IUserUpdateValidator
    {
        bool ValidateEmail(string? email);
        bool ValidateLogin(string? login);
        Task<bool> ValidatePasswordAsync(Guid userUUID, string? newPassword, string? oldPassword, CancellationToken cancellationToken = default);
    }
}