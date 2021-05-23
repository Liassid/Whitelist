using Exiled.Events.EventArgs;
using System.IO;

namespace Whitelist
{
    class EventHandlers
    {
        private readonly Plugin plugin;
        public EventHandlers(Plugin plugin) => this.plugin = plugin;
        public void OnWaitingForPlayers()
        {
            Directory.CreateDirectory(Plugin.ListDirectory);
            if (!File.Exists(Plugin.ListPath))
            {
                File.WriteAllText(Plugin.ListPath, "");
            }
            Plugin.whitelistedPlayers = FileManager.ReadAllLinesList(Plugin.ListPath);
        }
        public void OnPreAuthenticating(PreAuthenticatingEventArgs ev)
        {
            if (Plugin.whitelistEnabled && ev.Flags == 0 && !Plugin.whitelistedPlayers.Contains(ev.UserId))
            {
                ev.Reject(Plugin.Singleton.Config.RejectReason, false);
            }
        }
    }
}
