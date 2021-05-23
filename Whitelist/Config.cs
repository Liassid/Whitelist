using Exiled.API.Interfaces;

namespace Whitelist
{
    public sealed class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public string RejectReason { get; set; } = "You are not whitelisted on this server.";
        public string KickReason { get; set; } = "[Auto-Kick] You are not whitelisted on this server.";
    }
}
