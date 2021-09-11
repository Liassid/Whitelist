using Exiled.API.Features;
using Exiled.Events.EventArgs;
using System;
using System.Collections.Generic;
using System.IO;

namespace Whitelist
{
    public class Plugin : Plugin<Config, Translation>
    {
        public override string Name { get; } = "Whitelist";
        public override string Author { get; } = "Liassid";
        public override Version Version { get; } = new Version(1, 1, 0);
        public override Version RequiredExiledVersion { get; } = new Version(3, 0, 0);

        public static Plugin Singleton;
        public static bool whitelistEnabled = false;
        public static List<string> whitelistedPlayers = new List<string>();
        public static string ListDirectory => Path.Combine(Paths.Configs, "Whitelist");
        public static string ListPath => Path.Combine(ListDirectory, "WhitelistedPlayers.txt");

        public override void OnEnabled()
        {
            Singleton = this;
            Exiled.Events.Handlers.Player.PreAuthenticating += OnPreAuthenticating;

            Directory.CreateDirectory(ListDirectory);
            if (!File.Exists(ListPath))
                File.WriteAllText(ListPath, "");
            whitelistedPlayers = FileManager.ReadAllLinesList(ListPath);

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.PreAuthenticating -= OnPreAuthenticating;
            Singleton = null;
            base.OnDisabled();
        }

        public void OnPreAuthenticating(PreAuthenticatingEventArgs ev)
        {
            if (whitelistEnabled && !whitelistedPlayers.Contains(ev.UserId) && !((CentralAuthPreauthFlags)ev.Flags).HasFlagFast(CentralAuthPreauthFlags.IgnoreWhitelist))
                ev.Reject(Singleton.Config.RejectReason, false);
        }
    }
}
