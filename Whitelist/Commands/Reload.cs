using CommandSystem;
using System;

namespace Whitelist.Commands
{
    class Reload : ICommand
    {
        public string Command { get; } = "reload";

        public string[] Aliases { get; } = Array.Empty<string>();

        public string Description { get; } = "Reloads whitelist";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Plugin.whitelistedPlayers = FileManager.ReadAllLinesList(Plugin.ListPath);
            response = Plugin.Singleton.Translation.WhitelistReloaded;
            return true;
        }
    }
}
