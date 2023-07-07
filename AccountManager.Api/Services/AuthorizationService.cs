using AccountManager.Api.Exceptions;
using AccountManager.Api.Interfaces;
using AccountContract = AccountManager.Contract.Models;
using AccountManager.Data;
using AccountManager.Data.Enum;
using AccountManager.Data.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Extensions;
using Newtonsoft.Json.Linq;
using ManagerContract = AccountManager.Contract.Models;

namespace AccountManager.Api.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly AccountManagerContext _db;
        private readonly IHashService _hashService;

        public AuthorizationService(AccountManagerContext db, IHashService hashService, IUserValidator userValidator)
        {
            _db = db.ThrowIfNull();
            _hashService = hashService.ThrowIfNull();
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            var user = await GetByEmailOrLoginAsync(email);
            if (user == null || !_hashService.Verify(password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException();
            }

            return user;
        }

        public async Task<User> RegisterAsync(AccountContract.UserCreateRequest request)
        {
            var existingUser = await GetUserByLogin(request.Login);
            if (existingUser != null)
            {
                throw new UserAlreadyExistsException(request.Login, ManagerContract.UserAttribute.Login);
            }

            existingUser = await GetUserByEmail(request.Email);
            if (existingUser != null)
            {
                throw new UserAlreadyExistsException(request.Email, ManagerContract.UserAttribute.Email);
            }

            var user = _db.Users.Add(new User
            {
                UUID = Guid.NewGuid(),
                Email = request.Email.ToLower(),
                Login = request.Login,
                PasswordHash = _hashService.Hash(request.Password),
                CreatedAtUtc = DateTime.UtcNow,
                Role = Role.User.GetDescription(),
                SubscriptionType = SubscriptionType.Basic,
            });

            await _db.SaveChangesAsync();

            return user.Entity;
        }

        private Task<User?> GetUserByEmail(string email)
        {
            var valueLowerCase = email.ToLower();

            return _db.Users.FirstOrDefaultAsync(x => x.Email == valueLowerCase);
        }

        private Task<User?> GetUserByLogin(string login)
        {
            var valueLowerCase = login.ToLower();

            return _db.Users.FirstOrDefaultAsync(x => x.Login == valueLowerCase);
        }

        private Task<User?> GetByEmailOrLoginAsync(string value)
        {
            var valueLowerCase = value.ToLower();
            return _db.Users.FirstOrDefaultAsync(x => x.Email == valueLowerCase || x.Login == valueLowerCase);
        }
    }
}
