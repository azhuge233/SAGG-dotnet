using System.Text.Json.Serialization;

namespace SAGG.Models {
	public class GameStat {
		[JsonPropertyName("name")]
		public string Name { get; set; }
		[JsonPropertyName("defaultvalue")]
		public int DefaultValue { get; set; }
		[JsonPropertyName("displayName")]
		public string DisplayName { get; set; }

	}

	public class AvailableGameStats {
		[JsonPropertyName("stats")]
		public List<GameStat> Stats { get; set; }
		[JsonPropertyName("achievements")]
		public List<Achievement> Achievements { get; set; }
	}

	public class InnerData {
		[JsonPropertyName("gameName")]
		public string GameName { get; set; }
		[JsonPropertyName("gameVersion")]
		public string GameVersion { get; set; }
		[JsonPropertyName("availableGameStats")]
		public AvailableGameStats AvailableGameStats { get; set; }
	}

	public class GetSchemaForGameResult {
		[JsonPropertyName("game")]
		public InnerData Game { get; set; }
	}
}
