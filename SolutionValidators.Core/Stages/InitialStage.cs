using CodeProblemAssistant.Contract.Clients;
using InternalTypes.Interfaces;
using Shared.Core.Enums;
using Shared.Core.Exceptions;
using SolutionValidators.Core.ContextModels;

namespace SolutionValidators.Core.Stages
{
    public class InitialStage : IRunStage
    {
        public CodeRunStage Stage { get; init; }

        private readonly ICodeProblemAssistantClient _assistantClient;
        private readonly ITestCaseConverter _testCaseConverter;

        public InitialStage(
            ICodeProblemAssistantClient assistantClient,
            ITestCaseConverter testCaseConverter)
        {
            Stage = CodeRunStage.Queued;
            _assistantClient = assistantClient;
            _testCaseConverter = testCaseConverter;
        }

        public IRunContext Execute(IRunContext context)
        {
            var codeRun = context.CodeRun;
            var testCases = Task.Run(() => codeRun.RunTypeId == RunType.Submit
                ? _assistantClient.GetSubmitTestCasesByCodeProblemUUIDAsync(codeRun.CodeProblemReferenceUUID)
                : _assistantClient.GetRunTestCasesByCodeProblemUUIDAsync(codeRun.CodeProblemReferenceUUID)).GetAwaiter().GetResult();

            try
            {
                context.CodeProblem = _assistantClient.GetCodeProblemByUUIDAsync(codeRun.CodeProblemReferenceUUID).GetAwaiter().GetResult();
            }
            catch (ClientUnexpectedErrorException)
            {
                context.Challenge = _assistantClient.GetChallengeAsync(codeRun.CodeProblemReferenceUUID, codeRun.UserReferenceUUID).GetAwaiter().GetResult();
            }
            var methodInfo = context.CodeProblem?.MethodInfo ?? context.Challenge.MethodInfo;
            context.TestCases = _testCaseConverter.Convert(
                context.CodeRun.CodeLanguageId,
                methodInfo.Parameters.Select(p => p.Type).ToList(),
                methodInfo.ReturnType,
                testCases);

            return context;
        }
    }
}
