using CommandSystem;
using Exiled.API.Features;
using System;
using System.Linq;

namespace Whitelist.Commands
{
    class Toggle : ICommand
    {
        public string Command { get; } = "toggle";

        public string[] Aliases { get; } = Array.Empty<string>();

        public string Description { get; } = "Toggles whitelist status";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Plugin.whitelistEnabled = !Plugin.whitelistEnabled;
            ServerConsole.WhiteListEnabled = Plugin.whitelistEnabled;

            if (Plugin.whitelistEnabled)
                foreach (Player ply in Player.List.Where(x => !Plugin.whitelistedPlayers.Contains(x.UserId) && !x.IsStaffBypassEnabled))
                    ply.Disconnect(Plugin.Singleton.Config.KickReason);

            response = Plugin.whitelistEnabled ? Plugin.Singleton.Translation.WhitelistEnabled : Plugin.Singleton.Translation.WhitelistDisabled;
            return true;
        }
    }
}
