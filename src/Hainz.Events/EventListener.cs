using Hainz.Hosting;
using Microsoft.Extensions.Logging;

namespace Hainz.Events;

public class EventListener : GatewayServiceBase
{
    private readonly IEnumerable<INotificationSource> _notificationSources;
    private readonly ILogger<EventListener> _logger;

    public EventListener(IEnumerable<INotificationSource> notificationSources,
                         ILogger<EventListener> logger)
    {
        _logger = logger;
        _notificationSources = notificationSources;
    }

    public override async Task StartAsync()
    {
        _logger.LogInformation("Registering notification sources");

        int numNotificationSources = 0;
        foreach (var notificationSource in _notificationSources)
        {
            _logger.LogTrace("Registering notification source {name}", notificationSource.GetType().FullName);
            numNotificationSources++;
            await notificationSource.RegisterAsync();
        }

        _logger.LogInformation("Registered {count} notification sources", numNotificationSources);
    }

    public override async Task StopAsync()
    {
        _logger.LogInformation("Deregistering notification sources");

        int numNotificationSources = 0;
        foreach (var notificationSource in _notificationSources)
        {
            _logger.LogTrace("Deregistering notification source {name}", notificationSource.GetType().FullName);
            numNotificationSources++;
            await notificationSource.DeregisterAsync();
        }

        _logger.LogInformation("Deregistered {count} notification sources", numNotificationSources);
    }
}