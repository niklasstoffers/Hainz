using Discord;
using Hainz.Common.Collections;

namespace Hainz.Helpers.Discord;

public static class UserStatusMap
{
    private static readonly BidirectionalMap<string, UserStatus> _map = new() 
    {
        { "online", UserStatus.Online },
        { "offline", UserStatus.Offline },
        { "afk", UserStatus.AFK },
        { "idle", UserStatus.Idle },
        { "donotdisturb", UserStatus.DoNotDisturb },
        { "invisible", UserStatus.Invisible }
    };

    public static UserStatus? UserStatusFromString(string status) => _map.Forward.GetValueOrDefault(status);
    public static string? StringFromUserStatus(UserStatus status) => _map.Reverse.GetValueOrDefault(status);
}