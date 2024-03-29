using System.Reflection;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;

namespace Hainz.Hosting;

public sealed class GatewayServiceHost<TService> : IGatewayServiceHost<TService> where TService : IGatewayService
{
    private readonly DiscordSocketClient _client;
    private readonly TService _service;
    private readonly ILogger<GatewayServiceHost<TService>> _logger;
    private readonly string? _serviceName;
    private readonly RequireGatewayConnectionAttribute? _requireGatewayConnectionAttribute;
    private bool _isServiceRunning = false;

    public GatewayServiceHost(DiscordSocketClient client,
                              TService service,
                              ILogger<GatewayServiceHost<TService>> logger)
    {
        _client = client;
        _service = service;
        _logger = logger;

        var serviceType = service.GetType();

        _serviceName = serviceType.FullName;
        _requireGatewayConnectionAttribute = serviceType.GetCustomAttribute<RequireGatewayConnectionAttribute>();
    }

    public async Task StartAsync()
    {
        _logger.LogInformation("Starting service host for service \"{name}\"", _serviceName);

        await SetupServiceAsync();

        if (_requireGatewayConnectionAttribute == null)
        {
            await StartServiceAsync();
        }
        else 
        {
            _client.Ready += StartServiceAsync;
            _client.Disconnected += StopServiceAsync;
        }
    }

    public async Task StopAsync()
    {
        _logger.LogInformation("Stopping service host for service \"{name}\"", _serviceName);
        await StopServiceAsync(null);

        if (_requireGatewayConnectionAttribute != null)
        {
            _client.Ready -= StartServiceAsync;
            _client.Disconnected -= StopServiceAsync;
        }
    }

    private async Task<bool> SetupServiceAsync()
    {
        try
        {
            _logger.LogInformation("Setting up service \"{name}\"", _serviceName);
            await _service.SetupAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while setting up service \"{name}\"", _serviceName);
            return false;
        }
    }

    private async Task StartServiceAsync()
    {
        if (_isServiceRunning)
            return;

        try 
        {
            _logger.LogInformation("Starting service \"{name}\"", _serviceName);
            await _service.StartAsync();
            
            _isServiceRunning = true;
        }
        catch (Exception ex) 
        {
            _logger.LogError(ex, "Error occurred while trying to start service \"{name}\"", _serviceName);
        }
    }

    private async Task StopServiceAsync(Exception? stopException)
    {
        if (!_isServiceRunning)
            return;

        if (stopException == null) 
        {
            _logger.LogInformation("Stopping service \"{name}\"", _serviceName);
        }
        else 
        {
            _logger.LogInformation(stopException, "Stopping service \"{name}\" due to exception", _serviceName);
        }

        try 
        {
            await _service.StopAsync();
            _isServiceRunning = false;
        }
        catch (Exception ex) 
        {
            _logger.LogError(ex, "Error occurred while trying to stop service \"{name}\"", _serviceName);
        }
    }
}