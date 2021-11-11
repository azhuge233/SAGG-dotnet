using HtmlAgilityPack;
using SAGG.Models;
using SAGG.Modules;

namespace SAGG.Services {
	internal class Parser : IDisposable {
		internal Tuple<string, List<Achievements>> Parse(HtmlDocument htmlDoc) {
			try {
				var achievements = new List<Achievements>();
				Output.Info(Strings.ParseStrings.ParseStart);

				var gameTitle = htmlDoc.DocumentNode.SelectSingleNode(Strings.ParseStrings.GameTitleXPath).InnerText;
				var achievementIcons = htmlDoc.DocumentNode.SelectNodes(Strings.ParseStrings.AchievementIconXPath);
				var achievementNames = htmlDoc.DocumentNode.SelectNodes(Strings.ParseStrings.AchievementNameXPath);
				var achievementDescripts = htmlDoc.DocumentNode.SelectNodes(Strings.ParseStrings.AchievementDescriptXPath);

				for (int i = 0; i < achievementNames.Count; i++)
					achievements.Add(
						new Achievements(
							name: achievementNames[i].InnerText.Trim(),
							description: achievementDescripts[i].InnerText.Trim(),
							uri: achievementIcons[i].Attributes["src"].Value
						)
					);

				Output.Success(Strings.ParseStrings.ParseDone);
				return new Tuple<string, List<Achievements>>(gameTitle, achievements);
			} catch (Exception) {
				Output.Error(Strings.ParseStrings.ParseError);
				throw;
			} finally { 
				Dispose();
			}
		}

		public void Dispose() { 
			GC.SuppressFinalize(this);
		}
	}
}
