using Exiled.API.Interfaces;

namespace Whitelist
{
    public class Translation : ITranslation
    {
        public string InsufficientPermissions { get; set; } = "You don't have permissions to use this command";
        public string Usage { get; set; } = "Usage: whitelist <toggle/player/reload> [UserID]";
        public string WhitelistEnabled { get; set; } = "Whitelist enabled, all non-whitelisted players have been kicked and won't be able to connect";
        public string WhitelistDisabled { get; set; } = "Whitelist disabled";
        public string EnterUserID { get; set; } = "Enter a UserID";
        public string InvalidUserID { get; set; } = "Invalid UserID";
        public string PlayerAdded { get; set; } = "Player has been added to whitelist";
        public string PlayerRemoved { get; set; } = "Player has been removed from whitelist";
        public string WhitelistReloaded { get; set; } = "Whitelist reloaded";
    }
}
