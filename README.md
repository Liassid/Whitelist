# Whitelist
A simple plugin which allows server administrators to manage whitelist using RA commands.

**If you are hosting a Verified Server, you should set `custom_whitelist` to true in your `config_gameplay.txt`!**

## Commands
All commands require `whitelist.manage` permission.
| Command                   | Description                                          |
|---------------------------|------------------------------------------------------|
| whitelist enable          | Enables whitelist, kicks all non-whitelisted players |
| whitelist disable         | Disables whitelist                                   |
| whitelist player <UserID> | Adds/removes player from whitelist                   |
| whitelist reload          | Reloads whitelist                                    |
