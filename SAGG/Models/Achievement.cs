using System.Text.Json.Serialization;

namespace SAGG.Models {
	public class Achievement {
		[JsonPropertyName("name")]
		public string Name { get; set; }
		[JsonPropertyName("defaultvalue")]
		public int DefaultValue { get; set; }
		[JsonPropertyName("displayName")]
		public string DisplayName { get; set; }
		[JsonPropertyName("hidden")]
		public int Hidden { get; set; }
		[JsonPropertyName("description")]
		public string Description { get; set; }
		[JsonPropertyName("icon")]
		public Uri IconUri { get; set; }
		[JsonPropertyName("icongray")]
		public Uri IconGrayUri { get; set; }
	}
}
