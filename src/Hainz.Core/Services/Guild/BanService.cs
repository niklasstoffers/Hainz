using Discord;
using Discord.WebSocket;
using Hainz.Common.Helpers;
using Hainz.Core.Services.Messages;
using Hainz.Data.Queries.Guild.Bans.SendDMUponBan;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hainz.Core.Services.Guild;

public sealed class BanService
{
    private readonly DMService _dmService;
    private readonly IMediator _mediator;
    private readonly ILogger<BanService> _logger;

    public BanService(DMService dmService,
                      IMediator mediator,
                      ILogger<BanService> logger)
    {
        _dmService = dmService;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task<bool> BanAsync(SocketGuildUser user, string? reason = null, int? pruneDays = 0) =>
        await BanInternalAsync(user.Guild, user.Id, reason, pruneDays);

    public async Task<bool> BanAsync(SocketGuild guild, ulong userId, string? reason = null, int? pruneDays = 0) =>
        await BanInternalAsync(guild, userId, reason, pruneDays);

    public async Task<bool> UnbanAsync(SocketGuild guild, ulong userId) 
    {
        return await TryWrapper.TryAsync(
            async () => await guild.RemoveBanAsync(userId),
            ex => _logger.LogError(ex, "Exception while trying to unban user")
        );
    }

    private async Task<bool> BanInternalAsync(SocketGuild guild, ulong userId, string? reason, int? pruneDays)
    {
        // Note: We need to send the DM ban message first because after the user has been banned from the server the bot doesn't have the permission to send DMs to that user anymore
        var sendDMUponBan = await TryWrapper.TryAsync(
            async () => await _mediator.Send(new SendDMUponBanQuery(guild.Id)),
            onFailed: ex => _logger.LogError(ex, "SendDMUponBan query failed"));
        
        if (sendDMUponBan)
        {
            string dmMessage = $"You have been banned from \"{guild.Name}\". ";
            if (reason != null)
                dmMessage += $"Reason: \"{reason}\"";

            await _dmService.SendDMAsync(userId, dmMessage);
        }

        return await TryWrapper.TryAsync(
            async () => await guild.AddBanAsync(userId, pruneDays ?? 0, reason),
            ex => _logger.LogError(ex, "Exception while banning user")
        );
    }
}