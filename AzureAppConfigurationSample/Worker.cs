using Microsoft.Extensions.Options;

namespace AzureAppConfigurationSample
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly MyApplicationConfiguration _configuration;

        public Worker(ILogger<Worker> logger, IOptionsSnapshot<MyApplicationConfiguration> options)
        {
            _logger = logger;
            _configuration = options.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    _logger.LogInformation("App name: {name}", _configuration.ApplicationName);
                    _logger.LogInformation("App sercret: {sercret}", _configuration.ApplicationSecret);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
