using CommandSystem;
using Exiled.Permissions.Extensions;
using System;

namespace Whitelist.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Whitelist : ParentCommand, IUsageProvider
    {
        public Whitelist() => LoadGeneratedCommands();
        public override string Command { get; } = "whitelist";

        public override string[] Aliases { get; } = { "wlist", "wl" };

        public override string Description { get; } = "Command to manage Whitelist";

        public string[] Usage { get; } = { "SubCommand" };

        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new Toggle());
            RegisterCommand(new Ply());
            RegisterCommand(new Reload());
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("whitelist.manage"))
            {
                response = Plugin.Singleton.Translation.InsufficientPermissions;
                return false;
            }

            response = Plugin.Singleton.Translation.Usage;
            return false;
        }
    }
}
