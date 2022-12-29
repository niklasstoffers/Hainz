using Discord.Commands;
using Hainz.Commands.Metadata;
using Hainz.Core.Services.Status;

namespace Hainz.Commands.Modules.Bot;

public sealed class SetGameCommand : BotCommandBase
{
    private readonly ActivityService _activityService;

    public SetGameCommand(ActivityService activityService)
    {
        _activityService = activityService;
    }

    [Command("setgame")]
    [Summary("Sets the status game of the bot")]
    public async Task SetGameAsync([Remainder, CommandParameter(CommandParameterType.Text, "game", "The games name")] string game) 
    {
        if (await _activityService.SetGameAsync(game))
        {
            await Context.Channel.SendMessageAsync($"Set game to \"{game}\"");
        }
        else
        {
            await Context.Channel.SendMessageAsync($"Failed to set game to \"{game}\"");
        }
    }
}