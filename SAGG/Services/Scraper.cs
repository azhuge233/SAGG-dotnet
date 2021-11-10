using System.Text;
using System.Collections.Generic;
using HtmlAgilityPack;
using SAGG.Models;
using SAGG.Modules;

namespace SAGG.Services {
	internal class Scraper: IDisposable {
		internal List<Achievements> Scrape(string appID) {
			try {
				string fullUrl = new StringBuilder().AppendFormat(Strings.ScrapeStrings.SteamUrl, appID).ToString();
				var result = new List<Achievements>();


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
