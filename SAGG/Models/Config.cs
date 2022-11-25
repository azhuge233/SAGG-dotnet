using System.Text.Json.Serialization;

namespace SAGG.Models {
	public class Config {
		[JsonPropertyName("token")]
		public string Token { get; set; }
		[JsonPropertyName("language")]
		public string PreferedLanguage { get; set; }
	}
}
