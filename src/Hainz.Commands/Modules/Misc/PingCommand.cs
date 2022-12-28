using Discord.Commands;

namespace Hainz.Commands.Modules.Misc;

public sealed class PingCommand : MiscCommandBase
{
    [Command("ping")]
    [Summary("Responds with a message")]
    public async Task PingAsync() 
    {
        await Context.Channel.SendMessageAsync("pong");
    }
}