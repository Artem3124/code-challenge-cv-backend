using CodeProblemAssistant.Contract.Models;
using CodeProblemAssistant.Data.Enums;
using DataModels = CodeProblemAssistant.Data.Models;

namespace CodeProblemAssistant.Api.Mappers
{
    public class CodeProblemMethodInfoMapper
    {
        public CodeProblemMethodInfo Map(DataModels.Challenge challenge)
        {

            try
            {
                var parameterNamesList = challenge.ParameterNames.Split(',').Select(p => p.Trim()).ToList();
                var parameterTypesList = challenge.ParameterTypes.Split(',').Select(p => p.Trim()).ToList();
                var parameters = new List<CodeProblemParameterInfo>();
                for (int i = 0; i < parameterNamesList.Count; i++)
                {
                    parameters.Add(new CodeProblemParameterInfo
                    {
                        Name = parameterNamesList[i],
                        Type = (InternalType)Enum.Parse(typeof(InternalType), parameterTypesList[i]),
                    });
                }
                return new CodeProblemMethodInfo
                {
                    Name = string.Join(string.Empty, challenge.Name.Split(" ").Select(word => string.Concat(word[0].ToString().ToUpper(), word.AsSpan(1)))),
                    ReturnType = challenge.ReturnType,
                    Parameters = parameters,
                };
            }
            catch (Exception ex)
            {
                return new();
            }

        }

        public CodeProblemMethodInfo Map(DataModels.CodeProblem codeProblem)
        {
            var parameterNamesList = codeProblem.ParameterNamesCsv.Split(',').Select(p => p.Trim()).ToList();
            var parameterTypesList = codeProblem.ParameterTypesCsv.Split(',').Select(p => p.Trim()).ToList();
            var parameters = new List<CodeProblemParameterInfo>();
            for (int i = 0; i < parameterNamesList.Count; i++)
            {
                parameters.Add(new CodeProblemParameterInfo
                {
                    Name = parameterNamesList[i],
                    Type = (InternalType)Enum.Parse(typeof(InternalType), parameterTypesList[i]),
                });
            }
            return new CodeProblemMethodInfo
            {
                Name = string.Join(string.Empty, codeProblem.Name.Split(" ").Select(word => string.Concat(word[0].ToString().ToUpper(), word.AsSpan(1)))),
                ReturnType = codeProblem.ReturnType,
                Parameters = parameters,
            };
        }
    }
}
