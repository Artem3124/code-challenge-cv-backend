using InternalTypes.Converters;
using InternalTypes.Interfaces;
using InternalTypes.Types;
using Microsoft.Extensions.DependencyInjection;

namespace InternalTypes.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseInternalTypes(this IServiceCollection services) => services
            .AddScoped<ITestCaseConverter, TestCaseConverter>()
            .AddScoped<ITypeConverter, TypeConverter>()
            .AddScoped<ITypeSetFactory, TypeSetFactory>();
    }
}
