using SAGG.Models;
using SAGG.Modules;
using SAGG.Strings;
using System.Text.Json;

namespace SAGG.Services {
	internal class Parser : IDisposable {
		internal Tuple<string, List<Achievement>> Parse(string jsonString) {
			try {
				var jsonData = JsonSerializer.Deserialize<GetSchemaForGameResult>(jsonString).Game;
				var achievements = jsonData.AvailableGameStats.Achievements;
				Output.Info(ParseStrings.ParseStart);

				Output.Success(ParseStrings.ParseDone);
				return new Tuple<string, List<Achievement>>(jsonData.GameName, achievements);
			} catch (Exception) {
				Output.Error(ParseStrings.ParseError);
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
