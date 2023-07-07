using CodeProblemAssistant.Api.Interfaces;
using CodeProblemAssistant.Api.Mappers;
using CodeProblemAssistant.Contract.Models;
using CodeProblemAssistant.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Extensions;

namespace CodeProblemAssistant.Api.Services
{
    public class TagService : ITagService
    {
        private readonly CodeProblemAssistantContext _db;
        private readonly ITagMapper _mapper;

        public TagService(CodeProblemAssistantContext db, ITagMapper mapper)
        {
            _db = db.ThrowIfNull();
            _mapper = mapper.ThrowIfNull();
        }

        public async Task<List<Tag>> Query(TagQueryRequest request, CancellationToken cancellationToken = default)
        {
            var query = _db.Tags.AsQueryable();

            if (request.Id.HasValue)
            {
                query = query.Where(t => t.Id == request.Id);
            }
            if (request.Name != default)
            {
                var nameLowerCase = request.Name.ToLower();
                query = query.Where(t => t.Name == nameLowerCase);
            }

            var result = await query.ToListAsync(cancellationToken);

            return _mapper.Map(result);
        }

        public async Task<Tag> GetOrCreateAsync(string name, int codeProblemId, CancellationToken cancellationToken = default)
        {
            var nameLowerCase = name.ToLower();
            var codeProblem = _db.CodeProblems.Find(codeProblemId);
            var tag = await FindTagAsync(name, cancellationToken);
            tag ??= _db.Tags.Add(new Data.Models.Tag
            {
                CreatedAtUtc = DateTime.UtcNow,
                Name = nameLowerCase,
                UUID = Guid.NewGuid(),
            }).Entity;
            tag.CodeProblems.Add(codeProblem);
            await _db.SaveChangesAsync(cancellationToken);

            return _mapper.Map(tag);
        }

        public async Task<List<Tag>> GetOrCreateBatchAsync(List<string> names, Guid codeProblemUUID, CancellationToken cancellationToken = default)
        {
            var tags = new List<Data.Models.Tag>();
            var codeProblem = await _db.CodeProblems.FirstOrDefaultAsync(p => p.UUID == codeProblemUUID, cancellationToken);
            foreach(var name in names)
            {
                var nameLowerCase = name.ToLower();
                var tag = await FindTagAsync(nameLowerCase, cancellationToken);
                tag ??= _db.Tags.Add(new Data.Models.Tag
                {
                    CreatedAtUtc = DateTime.UtcNow,
                    Name = nameLowerCase,
                    UUID = Guid.NewGuid(),
                }).Entity;
                tag.CodeProblems.Add(codeProblem);
                tags.Add(tag);
            }

            await _db.SaveChangesAsync(cancellationToken);

            return _mapper.Map(tags);
        }

        private Task<Data.Models.Tag?> FindTagAsync(string name, CancellationToken cancellationToken) =>
            _db.Tags.FirstOrDefaultAsync(t => t.Name == name && !t.RemovedAtUtc.HasValue, cancellationToken);
    }
}
