using Hainz.Logging.Services;
using NLog;
using NLog.Targets;

namespace Hainz.Logging.NLog.Targets;

[Target("DiscordChannel")]
public sealed class DiscordChannelLogTarget : TargetWithLayout
{
    private readonly DiscordChannelLoggerService? _channelLoggerService;

    // Nlog catch22 workaround
    public DiscordChannelLogTarget() { }

    public DiscordChannelLogTarget(DiscordChannelLoggerService channelLoggerService)
    {
        _channelLoggerService = channelLoggerService;
    }

    protected override void Write(LogEventInfo logEvent)
    {
        var message = Layout.Render(logEvent);
        _channelLoggerService?.LogMessage(message);
    }
}