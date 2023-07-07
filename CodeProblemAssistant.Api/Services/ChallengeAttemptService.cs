using CodeProblemAssistant.Contract.Models;
using CodeProblemAssistant.Data;
using CodeProblemAssistant.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Enums;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;

namespace CodeProblemAssistant.Api.Services
{
    public class ChallengeAttemptService : IChallengeAttemptService
    {
        private readonly CodeProblemAssistantContext _db;

        public ChallengeAttemptService(CodeProblemAssistantContext db)
        {
            _db = db.ThrowIfNull();
        }

        public async Task SubmitAsync(ChallengeSubmitRequest request, CancellationToken cancellationToken = default)
        {
            var challenge = await FindAndValidateAsync(request.ChallengeUUID, request.UserUUID, cancellationToken);

            var attempt = challenge.Attempts.Single(a => a.UserUUID == request.UserUUID);

            attempt.State = Data.Enums.ChallengeSubmitState.InReview;
            attempt.SubmittedDateTimeUtc = DateTime.UtcNow;
            attempt.CodeLanguage = request.CodeLanguage;
            attempt.SourceCode = request.SourceCode;

            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task<Guid> CreateAsync(ChallengeCreateRequest request, CancellationToken cancellationToken = default)
        {
            var lowerCaseName = request.Name.ToLower().Trim();
            var existingChallenge = await _db.Challenges.FirstOrDefaultAsync(c => c.Name == lowerCaseName, cancellationToken);
            if (existingChallenge is not null)
            {
                throw new Exception("Challenge already exists");
            }

            var challenge = new Data.Models.Challenge
            {
                Name = lowerCaseName,
                Description = request.Description,
                AllowInvalidSyntaxSubmission = request.AllowInvalidSyntaxSubmit,
                CreatedAtUtc = DateTime.UtcNow,
                IsPrivate = request.IsPrivate,
                AllowedLanguagesCsv = string.Join(',', request.AllowedLanguages),
                HostUUID = request.HostUUID,
                UUID = Guid.NewGuid(),
                EndDateTimeUtc = request.EndDateTimeUtc,
                TimeLimitMinutes = request.TimeLimitMinutes,
                ReturnType = request.MethodInfo.ReturnType,
                ParameterNames = string.Join(',', request.MethodInfo.Parameters.Select(p => p.Name)),
                ParameterTypes = string.Join(',', request.MethodInfo.Parameters.Select(p => p.Type)),
            };

            var id = _db.Challenges.Add(challenge).Entity.Id;

            if (request.IsPrivate && request.AllowedUsers?.Any() == true)
            {
                challenge.AllowedUsers.AddRange(request.AllowedUsers.Select(u => new Data.Models.PrivateChallengeAllowedUsers
                {
                    ChallengeId = id,
                    UserReferenceUUID = u,
                }));
            }

            await _db.SaveChangesAsync(cancellationToken);

            return challenge.UUID;
        }

        public async Task<Challenge> GetAsync(Guid challengeUUID, Guid userUUID, CancellationToken cancellationToken = default)
        {
            var challenge = await _db.Challenges
                .Include(c => c.AllowedUsers)
                .Include(c => c.Attempts)
                .AsSplitQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.UUID == challengeUUID, cancellationToken);

            if (challenge is null)
            {
                throw new ObjectNotFoundException(challengeUUID, nameof(challenge));
            }

            return Map(challenge, userUUID);
        }

        public async Task<ChallengeAttempt> StartAsync(ChallengeStartRequest request, CancellationToken cancellationToken = default)
        {
            var challenge = await FindAndValidateAsync(request.ChallengeUUID, request.UserUUID, cancellationToken);
            var attempt = new Data.Models.ChallengeAttempt
            {
                UserUUID = request.UserUUID,
                StartDateTimeUtc = DateTime.UtcNow,
                CreatedAtUtc = DateTime.UtcNow,
                UUID = Guid.NewGuid(),
            };

            if (challenge.Attempts.Any(a => a.UserUUID == request.UserUUID))
            {
                throw new Exception("Attempt is already started.");
            }

            challenge.Attempts.Add(attempt);

            await _db.SaveChangesAsync(cancellationToken);

            return new ChallengeAttempt
            {
                UserUUID = request.UserUUID,
                StartedDateTimeUtc = attempt.StartDateTimeUtc,
            };
        }

        public async Task<List<Challenge>> GetAsync(Guid userUUID, CancellationToken cancellationToken = default)
        {
            var challenges = await _db.Challenges
                .Include(c => c.AllowedUsers)
                .Include(c => c.Attempts)
                .AsSplitQuery()
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return challenges.Select(c => Map(c, userUUID)).ToList();
        }

        private async Task<Data.Models.Challenge> FindAndValidateAsync(Guid challengeUUID, Guid userUUID, CancellationToken cancellationToken)
        {
            var challenge = await _db.Challenges.Include(c => c.Attempts).AsSplitQuery().FirstOrDefaultAsync(c => c.UUID == challengeUUID, cancellationToken);
            if (challenge is null)
            {
                throw new ObjectNotFoundException(challengeUUID, nameof(challenge));
            }

            if (challenge.IsPrivate && !challenge.AllowedUsers.Any(u => u.UserReferenceUUID == userUUID))
            {
                throw new Exception("User is not allowed.");
            }

            return challenge;
        }

        private Challenge Map(Data.Models.Challenge c, Guid userUUID)
        {
            var parameterNames = c.ParameterNames.Split(',').ToList();
            var parameterTypes = c.ParameterTypes.Split(',').Select(p => (InternalType)Enum.Parse(typeof(InternalType), p)).ToList();

            var methodInfo = new CodeProblemMethodInfo
            {
                Name = c.Name,
                ReturnType = c.ReturnType,
                Parameters = new(),
            };
            for (int i = 0; i < parameterNames.Count; i++)
            {
                methodInfo.Parameters.Add(new CodeProblemParameterInfo
                {
                    Name = parameterNames[i],
                    Type = parameterTypes[i],
                });
            }
            return new()
            {
                Name = c.Name,
                Description = c.Description,
                AllowInvalidSyntaxSubmission = c.AllowInvalidSyntaxSubmission,
                AllowedLanguages = c.AllowedLanguagesCsv.Split(',').Select(l => (CodeLanguage)Enum.Parse(typeof(CodeLanguage), l)).ToList(),
                EndDateTimeUtc = c.EndDateTimeUtc,
                AllowedUsers = c.AllowedUsers.Select(u => u.UserReferenceUUID).ToList(),
                HostUUID = c.HostUUID,
                TimeLimitMinutes = c.TimeLimitMinutes,
                IsPrivate = c.IsPrivate,
                UUID = c.UUID,
                MethodInfo = methodInfo,
                UserAttempt = c.Attempts.Where(a => a.UserUUID == userUUID).Select(a => new ChallengeAttempt
                {
                    UUID = c.UUID,
                    SourceCode = a.SourceCode,
                    StartedDateTimeUtc = a.StartDateTimeUtc,
                    State = a.State,
                    SubmittedDateTimeUtc = a.SubmittedDateTimeUtc,
                    UserUUID = a.UserUUID,
                    CodeLanguage = a.CodeLanguage,
                }).FirstOrDefault(),
                Attempts = c.Attempts.Select(a => new ChallengeAttempt
                {
                    UUID = a.UUID,
                    SourceCode = a.SourceCode,
                    StartedDateTimeUtc = a.StartDateTimeUtc,
                    State = a.State,
                    SubmittedDateTimeUtc = a.SubmittedDateTimeUtc,
                    UserUUID = a.UserUUID,
                }).ToList(),
            };
        }

        public async Task<ChallengeAttempt> GetAttemptAsync(Guid uuid, CancellationToken cancellationToken = default)
        {
            var attempt = await _db.Attempts.FirstOrDefaultAsync(c => c.UUID == uuid, cancellationToken);
            if (attempt is null)
            {
                throw new ObjectNotFoundException(uuid, nameof(attempt));
            }

            return new ChallengeAttempt
            {
                UUID = attempt.UUID,
                CodeLanguage = attempt.CodeLanguage,
                SourceCode = attempt.SourceCode,
                StartedDateTimeUtc = attempt.StartDateTimeUtc,
                SubmittedDateTimeUtc = attempt.SubmittedDateTimeUtc,
                State = attempt.State,
                UserUUID = attempt.UserUUID,
            };
        }

        public async Task<bool> Patch(Guid uuid, ChallengeUpdateRequest request, CancellationToken cancellationToken = default)
        {
            var attempt = await _db.Attempts.FirstOrDefaultAsync(c => c.UUID == uuid, cancellationToken);
            if (attempt is null)
            {
                throw new ObjectNotFoundException(uuid, nameof(attempt));
            }

            attempt.State = request.State;

            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
