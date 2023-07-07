using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mongo.Data.Services;
using Shared.Core.Extensions;
using WorkerService.Stager.Interfaces;

namespace WorkerService.Stager.Services
{
    internal class QueueChecker
    {
        private readonly ILogger<QueueChecker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private int _delay = 5000;

        public QueueChecker(IServiceProvider serviceProvider, ILogger<QueueChecker> logger)
        {
            _logger = logger.ThrowIfNull();
            _serviceProvider = serviceProvider;
        }

        public async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await DequeueAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unexpected error occurred while checking core runs queue. Recovering...");

                    await Task.Delay(_delay, stoppingToken);
                }
            }
        }

        private async Task DequeueAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var queueService = scope.ServiceProvider.GetRequiredService<ICodeRunQueueMessageService>();

            var item = await queueService.DequeueAsync(cancellationToken);

            if (item == null)
            {
                _logger.LogInformation("No items were found to run.");

                await Task.Delay(_delay, cancellationToken);
            }
            else
            {
                _logger.LogInformation("Initializing check for {runUUID}", item.UUID);

                var stager = scope.ServiceProvider.GetRequiredService<IStageService>();

                await stager.Start(item, cancellationToken);

                _logger.LogInformation("Check finished for {runUUID}", item.UUID);
            }
        }
    }
}
