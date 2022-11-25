# SAGG-dotnet
Steam Achievement Guide Generator

This CLI tool

- Fetch given steam game's achievements list, then save each achievement's title and description in a txt file under a folder named by this achievement.
- Download each achievement's icon, resize it to 64x64 (so it can be displayed in a reasonable size), then save it next to each .txt file.


## Build

Install dotnet 7.0 SDK first, you can find installation packages/guides [here](https://dotnet.microsoft.com/download).

### Publish

```
dotnet publish -c Release -o /your/path/here -r [win-x64/osx-x64/linux-x64/...] --sc
```

Or use pre-build package in [Releases](https://github.com/azhuge233/SAGG-dotnet/releases).

## Usage

1. Fill your Steam API Token in config.json, you can get your own token [here](https://steamcommunity.com/dev/apikey).

2. (Optional) Change language setting to your prefered language

```json
{
    "token": "XXXXXXXXXXXX",
    "language": "schinese" // e.g. english, french
}
```

Find Steam game's AppID (usually it can be found in the Store page's URL), then execute

```
SAGG [AppID]
```

Tested on Windows 10/11, macOS Catalina/Monterey, Debian 11.
