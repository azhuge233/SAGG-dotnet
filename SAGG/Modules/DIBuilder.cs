using Microsoft.Extensions.DependencyInjection;
using SAGG.Services;

namespace SAGG.Modules {
	internal static class DIBuilder {
		public static IServiceProvider Build() { 
			return new ServiceCollection()
				.AddTransient<Validator>()
				.AddTransient<Scraper>()
				.BuildServiceProvider();
		}
	}
}
