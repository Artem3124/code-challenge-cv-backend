using AccountManager.Api.Exceptions;
using AccountManager.Api.Interfaces;
using AccountManager.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;

namespace AccountManager.Api.Services
{
    public class UserUpdateValidator : IUserUpdateValidator
    {
        private readonly IUserValidator _userValidator;
        private readonly AccountManagerContext _db;
        private readonly IHashService _hashService;

        public UserUpdateValidator(IUserValidator userValidator, IHashService hashService, AccountManagerContext db)
        {
            _userValidator = userValidator.ThrowIfNull();
            _db = db.ThrowIfNull();
            _hashService = hashService.ThrowIfNull();
        }

        public bool ValidateLogin(string? login)
        {
            if (login == null)
            {
                return false;
            }

            _userValidator.ValidateLogin(login);

            return true;
        }

        public bool ValidateEmail(string? email)
        {
            if (email == null)
            {
                return false;
            }

            _userValidator.ValidateEmail(email);

            return true;
        }

        public async Task<bool> ValidatePasswordAsync(Guid userUUID, string? newPassword, string? oldPassword, CancellationToken cancellationToken = default)
        {
            if (newPassword == null)
            {
                return false;
            }

            var passwordHash = await _db.Users.Where(u => u.UUID == userUUID).Select(u => u.PasswordHash).FirstOrDefaultAsync(cancellationToken);
            if (passwordHash == null)
            {
                throw new ObjectNotFoundException(userUUID, "user");
            }

            if (!_hashService.Verify(oldPassword, passwordHash))
            {
                throw new PasswordMissmatchException();
            }

            return true;
        }
    }
}
