using CodeProblemAssistant.Api.Interfaces;
using CodeProblemAssistant.Api.Mappers;
using CodeProblemAssistant.Contract.Models;
using CodeProblemAssistant.Data;
using DataModels = CodeProblemAssistant.Data.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;
using Newtonsoft.Json;
using CodeProblemAssistant.Data.Enums;
using TestCases.Models;
using TestCases.Providers;

namespace CodeProblemAssistant.Api.Services
{
    internal class CodeProblemService : ICodeProblemService
    {
        private readonly CodeProblemAssistantContext _db;
        private readonly ICodeProblemMapper _mapper;
        private readonly ITestCasesProvider _testCasesProvider;

        public CodeProblemService(CodeProblemAssistantContext db, ICodeProblemMapper mapper, ITestCasesProvider testCasesProvider)
        {
            _db = db.ThrowIfNull();
            _mapper = mapper.ThrowIfNull();
            _testCasesProvider = testCasesProvider.ThrowIfNull();
        }

        public async Task<CodeProblem> GetAsync(Guid codeProblemUUID, CancellationToken cancellationToken = default)
        {
            var codeProblem = await _db.CodeProblems
                .AsSplitQuery()
                .Include(p => p.Tags)
                .Include(p => p.Votes)
                .FirstOrDefaultAsync(p => p.UUID == codeProblemUUID, cancellationToken);

            if (codeProblem == null)
            {
                throw new ObjectNotFoundException(codeProblemUUID, nameof(codeProblem));
            }

            return _mapper.Map(codeProblem);
        }

        public async Task<List<CodeProblem>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var codeProblems = await _db.CodeProblems
                .AsSplitQuery()
                .Include(p => p.Tags)
                .Include(p => p.Votes)
                .Where(p => !p.RemovedAtUtc.HasValue)
                .ToListAsync(cancellationToken);

            return _mapper.Map(codeProblems).ToList();
        }

        public async Task<List<TestCase>> GetRunTestCaseSetAsync(Guid codeProblemUUID, CancellationToken cancellationToken = default)
        {
            var codeProblem = await _db.CodeProblems
                           .Where(p => p.UUID == codeProblemUUID)
                           .FirstOrDefaultAsync(cancellationToken);

            var testCaseSetUUID = codeProblem?.TestCaseSetUUID;
            if (!testCaseSetUUID.HasValue)
            {
                testCaseSetUUID = (await _db.Challenges.FirstOrDefaultAsync(c => c.UUID == codeProblemUUID, cancellationToken))?.TestCaseReferenceUUID ?? Guid.NewGuid();

                if (!testCaseSetUUID.HasValue)
                {
                    throw new ObjectNotFoundException(codeProblemUUID, nameof(codeProblem));
                }
            }
            return _testCasesProvider.GetTestCaseSet(testCaseSetUUID.Value, 10);
        }

        public async Task<List<TestCase>> GetSubmitTestCaseSetAsync(Guid codeProblemUUID, CancellationToken cancellationToken = default)
        {
            var codeProblem = await _db.CodeProblems
                .Where(p => p.UUID == codeProblemUUID)
                .FirstOrDefaultAsync(cancellationToken);

            var testCaseSetUUID = codeProblem?.TestCaseSetUUID;
            if (!testCaseSetUUID.HasValue)
            {
                testCaseSetUUID = (await _db.Challenges.FirstOrDefaultAsync(c => c.UUID == codeProblemUUID, cancellationToken))?.TestCaseReferenceUUID ?? Guid.NewGuid();

                if (!testCaseSetUUID.HasValue)
                {
                    throw new ObjectNotFoundException(codeProblemUUID, nameof(codeProblem));
                }
            }

            return _testCasesProvider.GetTestCaseSet(testCaseSetUUID.Value);
        }

        public async Task<Guid> CreateAsync(CodeProblemCreateRequest request, CancellationToken cancellationToken = default)
        {
            var name = request.Name.ToLower().Trim();
            var isCodeProblemExists = await _db.CodeProblems.AnyAsync(p => p.Name == name, cancellationToken);

            if (isCodeProblemExists)
            {
                throw new InvalidOperationException($"Code problem {request.Name} already exists");
            }

            var codeProblem = new DataModels.CodeProblem
            {
                UUID = Guid.NewGuid(),
                Name = name,
                ComplexityTypeId = request.ComplexityTypeId,
                ExamplesJson = JsonConvert.SerializeObject(request.Examples),
                ConstraintsJson = JsonConvert.SerializeObject(request.Constraints),
                Description = request.Description,
                CreatedAtUtc = DateTime.UtcNow,
                ParameterNamesCsv = StringListToCsv(request.ParameterNames),
                ParameterTypesCsv = EnumListToCsv(request.ParameterTypes),
                ReturnType = request.ReturnType,
                TestCaseSetUUID = request.TestCaseSetUUID,
                Explanation = request.Explanation,
            };

            _db.CodeProblems.Add(codeProblem);
            try
            {
                await _db.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return codeProblem.UUID;
        }

        private string StringListToCsv(List<string> values) => string.Join(',', values);

        private string EnumListToCsv(List<InternalType> values) => string.Join(',', values);
    }
}
