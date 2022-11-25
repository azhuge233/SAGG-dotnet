using Microsoft.Extensions.DependencyInjection;
using SAGG.Services;

namespace SAGG.Modules {
	internal static class DIBuilder {
		internal static IServiceProvider Build() { 
			return new ServiceCollection()
				.AddTransient<Validator>()
				.AddTransient<Scraper>()
				.AddTransient<Parser>()
				.AddTransient<FileSaver>()
				.AddTransient<JsonLoader>()
				.BuildServiceProvider();
		}
	}
}
