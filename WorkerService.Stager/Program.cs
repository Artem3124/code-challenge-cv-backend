using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkerService.Stager.Configurations;
using WorkerService.Stager.Services;

var serviceCollection = new ServiceCollection();
var configuration = new ConfigurationBuilder()
  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
  .Build();

DiConfigurations.Configure(serviceCollection, configuration);

var serviceProvider = serviceCollection.BuildServiceProvider();

var queueChecker = serviceProvider.GetRequiredService<QueueChecker>();

var cancellationTokenSource = new CancellationTokenSource();
await queueChecker.ExecuteAsync(cancellationTokenSource.Token);