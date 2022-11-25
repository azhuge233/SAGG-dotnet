using System.Text.Json;
using SAGG.Models;

namespace SAGG.Services {
	internal class JsonLoader: IDisposable {
		private readonly string configPath = $"{AppDomain.CurrentDomain.BaseDirectory}config.json";

		internal Config LoadConfig() {
			try {
				var content = JsonSerializer.Deserialize<Config>(File.ReadAllText(configPath));
				return content;
			} catch (Exception) {
				throw;
			}
		}

		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
