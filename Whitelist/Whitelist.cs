using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;

namespace Whitelist
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    class Whitelist : ICommand
    {
        public string Command { get; } = "whitelist";

        public string[] Aliases { get; } = { "wlist", "wl" };

        public string Description { get; } = "Command to manage Whitelist";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("whitelist.manage"))
            {
                response = "You don't have permissions to use this command";
                return false;
            }

            if (arguments.Count < 1)
            {
                response = "Usage: whitelist <enable/disable/player/reload>";
                return false;
            }

            switch (arguments.At(0))
            {
                case "enable":
                    Plugin.whitelistEnabled = true;
                    foreach (Player ply in Player.List)
                    {
                        if (!Plugin.whitelistedPlayers.Contains(ply.UserId))
                        {
                            ply.Disconnect(Plugin.Singleton.Config.KickReason);
                        }
                    }
                    response = "Whitelist enabled, all non-whitelisted players have been kicked and won't be able to connect";
                    return true;
                case "disable":
                    Plugin.whitelistEnabled = false;
                    response = "Whitelist disabled";
                    return true;
                case "player":
                    if (arguments.Count != 2)
                    {
                        response = "Enter a UserID";
                        return false;
                    }

                    if (!(arguments.At(1).EndsWith("@steam") || arguments.At(1).EndsWith("@discord") || arguments.At(1).EndsWith("@northwood") || arguments.At(1).EndsWith("@patreon")))
                    {
                        response = "Invalid UserID";
                        return false;
                    }

                    Plugin.whitelistedPlayers = FileManager.ReadAllLinesList(Plugin.ListPath);

                    if (Plugin.whitelistedPlayers.Contains(arguments.At(1)))
                    {
                        Plugin.whitelistedPlayers.Remove(arguments.At(1));
                        FileManager.WriteToFile(Plugin.whitelistedPlayers, Plugin.ListPath);

                        response = "Player has been removed from whitelist";
                        return true;
                    }
                    else
                    {
                        Plugin.whitelistedPlayers.Add(arguments.At(1));
                        FileManager.WriteToFile(Plugin.whitelistedPlayers, Plugin.ListPath);

                        response = "Player has been added to whitelist";
                        return true;
                    }
                case "reload":
                    Plugin.whitelistedPlayers = FileManager.ReadAllLinesList(Plugin.ListPath);
                    response = "Whitelist reloaded";
                    return true;
            }
            response = "Usage: whitelist <enable/disable/player/reload>";
            return false;
        }
    }
}
