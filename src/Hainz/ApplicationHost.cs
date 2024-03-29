using Hainz.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Hainz;

public sealed class ApplicationHost : IHostedService
{
    private readonly IEnumerable<IGatewayServiceHost<IGatewayService>> _gatewayServiceHosts;
    private readonly ILogger<ApplicationHost> _logger;

    public ApplicationHost(IEnumerable<IGatewayServiceHost<IGatewayService>> gatewayServiceHosts,
                           ILogger<ApplicationHost> logger)
    {
        _gatewayServiceHosts = gatewayServiceHosts;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting application host");

        foreach (var serviceHost in _gatewayServiceHosts) 
        {
            await serviceHost.StartAsync();
        }

        _logger.LogInformation("Application host started");
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping application host");

        foreach (var serviceHost in _gatewayServiceHosts)
        {
            await serviceHost.StopAsync();
        }

        _logger.LogInformation("Application host stopped");
    }
}