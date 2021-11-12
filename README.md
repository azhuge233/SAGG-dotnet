# SAGG-dotnet
Steam Achievement Guide Generator

This CLI tool simply does:

- Fetch given steam game's achievements list, then save each achievement's title and description in a txt file under a folder named by this achievement.
- Download each achievement's icon, resize it to 64x64 (so it can be displayed in a reasonable size), then save it next to each .txt file.

You should modify the `Header` variable to your preferred language first if you don't want Simplified Chinese.

## Build

Install dotnet 6.0 SDK first, you can find installation packages/guides [here](https://dotnet.microsoft.com/download).

### Publish

```
dotnet publish -c Release -o /your/path/here -r [win-x64/osx-x64/...] --sc
# set PublishTrimmed property to false in Linux
dotnet publish -c Release -o /your/path/here -r linux-x64 --sc -p:PublishTrimmed=false
```

Or use pre-build package in [Releases](https://github.com/azhuge233/SAGG-dotnet/releases).

## Usage

Find Steam game's AppID (usually it can be found in the Store page's URL), then execute

```
SAGG [AppID]
```
