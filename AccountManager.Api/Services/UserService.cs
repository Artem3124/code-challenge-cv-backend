using AccountManager.Api.Interfaces;
using AccountManager.Api.Mappers;
using AccountManager.Contract.Models;
using AccountManager.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;
using System.Runtime.CompilerServices;

namespace AccountManager.Api.Services
{
    public class UserService : IUserService
    {
        private readonly AccountManagerContext _db;
        private readonly IUserMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserValidator _userValidator;
        private readonly IUserUpdateValidator _userUpdateValidator;
        private readonly IHashService _hashService;

        public UserService(
            AccountManagerContext db,
            IUserMapper mapper,
            IAuthorizationService authorizationService,
            IUserValidator userValidator,
            IUserUpdateValidator userUpdateValidator,
            IHashService hashService)
        {
            _db = db.ThrowIfNull();
            _mapper = mapper.ThrowIfNull();
            _authorizationService = authorizationService.ThrowIfNull();
            _userValidator = userValidator.ThrowIfNull();
            _userUpdateValidator = userUpdateValidator.ThrowIfNull();
            _hashService = hashService.ThrowIfNull();
        }

        public async Task<User> CreateAsync(UserCreateRequest request)
        {
            _userValidator.ValidateCreateUserRequest(request);
            var user = await _authorizationService.RegisterAsync(request);

            return _mapper.Map(user);
        }

        public async Task<User> GetAsync(Guid uuid)
        {
            var user = await FindOrThrowAsync(uuid);

            return _mapper.Map(user);
        }

        public async Task<List<User>> GetAsync()
        {
            return _mapper.Map(await _db.Users.ToListAsync());
        }

        public async Task<int> UpdateAsync(Guid uuid, UserUpdateRequest request)
        {
            var user = await FindOrThrowAsync(uuid);
            
            if (request.Email != null)
            {
                _userUpdateValidator.ValidateEmail(request.Email);
                UpdateChangeHistory(user.Id, user.Email);
                user.Email = request.Email;
            }
            if (request.Login != null)
            {
                _userUpdateValidator.ValidateLogin(request.Login);
                UpdateChangeHistory(user.Id, user.Login);
                user.Login = request.Login;
            }
            if (request.NewPassword != null)
            {
                await _userUpdateValidator.ValidatePasswordAsync(uuid, request.NewPassword, request.OldPassword);
                UpdateChangeHistory(user.Id, user.PasswordHash);
                user.PasswordHash = _hashService.Hash(request.NewPassword);
            }

            return await _db.SaveChangesAsync();
        }

        private async Task<Data.Models.User> FindOrThrowAsync(Guid uuid)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UUID == uuid);
            if (user == null)
            {
                throw new ObjectNotFoundException(uuid, nameof(user));
            }
            return user;
        }

        private void UpdateChangeHistory(int userId, string value, [CallerArgumentExpression("value")] string? columnName = default) => _db.UserChangeHistory.Add(new() 
        {
            ColumnName = columnName,
            DateTimeUtc = DateTime.UtcNow,
            OldValue = value,
            UserId = userId,
        });
    }
}
