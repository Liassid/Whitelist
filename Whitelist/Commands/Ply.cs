using CommandSystem;
using System;

namespace Whitelist.Commands
{
    class Ply : ICommand, IUsageProvider
    {
        public string Command { get; } = "player";

        public string[] Aliases { get; } = Array.Empty<string>();

        public string Description { get; } = "Adds or removes player from whitelist";

        public string[] Usage { get; } = { "UserID" };

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count != 1)
            {
                response = Plugin.Singleton.Translation.EnterUserID;
                return false;
            }

            if (!(arguments.At(0).EndsWith("@steam") || arguments.At(0).EndsWith("@discord") || arguments.At(0).EndsWith("@northwood") || arguments.At(0).EndsWith("@patreon")))
            {
                response = Plugin.Singleton.Translation.InvalidUserID;
                return false;
            }

            if (Plugin.whitelistedPlayers.Contains(arguments.At(0)))
            {
                Plugin.whitelistedPlayers.Remove(arguments.At(0));
                response = Plugin.Singleton.Translation.PlayerRemoved;
            }
            else
            {
                Plugin.whitelistedPlayers.Add(arguments.At(0));
                response = Plugin.Singleton.Translation.PlayerAdded;
            }
            FileManager.WriteToFile(Plugin.whitelistedPlayers, Plugin.ListPath);
            return true;
        }
    }
}
