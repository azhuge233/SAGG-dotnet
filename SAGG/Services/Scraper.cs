using System.Text;
using HtmlAgilityPack;
using SAGG.Modules;

namespace SAGG.Services {
	internal class Scraper: IDisposable {
		internal HtmlDocument Scrape(string appID) {
			try {
				Output.Info(Strings.ScrapeStrings.ScrapeStart);
				string fullUrl = new StringBuilder().AppendFormat(Strings.ScrapeStrings.SteamUrl, appID).ToString();

				var htmlWeb = new HtmlWeb();
				htmlWeb.UserAgent = Strings.ScrapeStrings.UA;
				htmlWeb.PreRequest += request => {
					request.Headers.Add(Strings.ScrapeStrings.Header);
					return true;
				};
				var result = htmlWeb.Load(fullUrl);

				Output.Success(Strings.ScrapeStrings.SrapeDone);
				return result;
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
