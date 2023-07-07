using CodeProblemPatcher;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var configuration = new ConfigurationBuilder()
  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
  .Build();

services.Config(configuration);

var serviceProvider = services.BuildServiceProvider();

var loader = serviceProvider.GetService<CodeProblemLoader>();
if (loader == null)
{
    throw new Exception("Unable to load.");
}

try
{
    Console.WriteLine("Loading...");
    await loader.Load();
}
catch (Exception ex)
{
    Console.WriteLine("An Exception occurred while loading data.");
    Console.WriteLine(ex.ToString());
    return;
}

Console.WriteLine("All done.");