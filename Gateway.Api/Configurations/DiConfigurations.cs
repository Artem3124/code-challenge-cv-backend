using AccountManager.Contract.Extensions;
using CodeProblemAssistant.Contract;
using CodeRunManager.Contract.Extensions;
using Gateway.Api.Interfaces;
using Gateway.Api.Providers;
using Gateway.Api.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Shared.Core.Enums;

namespace Gateway.Api.Configurations
{
    public static class DiConfigurations
    {
        public static IServiceCollection Configure(IServiceCollection services, IConfiguration configuration) => services
            .AddLogging()
            .AddMappers()
            .AddScoped<ICodeSubmitService, CodeSubmitService>()
            .AddScoped<ICodeProblemProvider, CodeProblemProvider>()
            .AddScoped<ICodeRunResultService, CodeRunResultService>()
            .AddCodeRunManagerClient(configuration.GetValue<string>("Endpoints:CodeRunManagerClient"))
            .AddCodeProblemAssistantClient(configuration.GetValue<string>("Endpoints:CodeProblemAssistantClient"))
            .AddAccountManager(configuration.GetValue<string>("Endpoints:AccountManagerClient"))
            .AddScoped<ICodeLanguageTypeValidator, CodeLanguageTypeValidator>(cfg => new CodeLanguageTypeValidator(
                new()
                {
                    CodeLanguage.Cs,
                    CodeLanguage.Cpp,
                    CodeLanguage.Python,
                }))
            .AddScoped<ICodeTemplateService, CodeTemplateService>()
            .AddScoped<IGatewayAuthorizationService>(cfg => new GatewayAuthorizationService(CookieAuthenticationDefaults.AuthenticationScheme));
    }
}
