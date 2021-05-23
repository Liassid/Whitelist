using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.IO;

namespace Whitelist
{
    public class Plugin : Plugin<Config>
    {
        private EventHandlers handler;
        public override string Name { get; } = "Whitelist";
        public override string Author { get; } = "Liassid";
        public override Version Version { get; } = new Version(1, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(2, 10, 0);

        public static Plugin Singleton;

        public static bool whitelistEnabled;
        public static List<string> whitelistedPlayers = new List<string>();
        public static string ListDirectory => Path.Combine(Paths.Configs, "Whitelist");
        public static string ListPath => Path.Combine(ListDirectory, "WhitelistedPlayers.txt");

        public override void OnEnabled()
        {
            Singleton = this;
            handler = new EventHandlers(this);
            Exiled.Events.Handlers.Server.WaitingForPlayers += handler.OnWaitingForPlayers;
            Exiled.Events.Handlers.Player.PreAuthenticating += handler.OnPreAuthenticating;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Server.WaitingForPlayers -= handler.OnWaitingForPlayers;
            Exiled.Events.Handlers.Player.PreAuthenticating -= handler.OnPreAuthenticating;
            Singleton = null;
            handler = null;
            base.OnDisabled();
        }
    }
}
