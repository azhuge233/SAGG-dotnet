using System.Text;
using SAGG.Modules;
using SAGG.Models;

namespace SAGG.Services {
	internal class Scraper: IDisposable {
		internal async Task<string> Scrape(Config config, string appId) {
			try {
				Output.Info(Strings.ScrapeStrings.ScrapeStart);
				string fullUrl = new StringBuilder().AppendFormat(Strings.ScrapeStrings.SteamUrl, config.Token, appId, config.PreferedLanguage).ToString();

				using var client = new HttpClient();
				var response = await client.GetAsync(fullUrl);

				Output.Success(Strings.ScrapeStrings.SrapeDone);
				return await response.Content.ReadAsStringAsync();
			} catch (Exception) {
				Output.Error(Strings.ScrapeStrings.ScrapeError);
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
