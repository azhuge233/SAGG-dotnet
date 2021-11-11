using Microsoft.Extensions.DependencyInjection;
using SAGG.Modules;
using SAGG.Services;

namespace SAGG {
    class Program {
         static async Task Main(string[] args) {
            try {
                var services = DIBuilder.Build();

                using (services as IDisposable) {
                    if (!services.GetRequiredService<Validator>().Validate(args)) {
                        throw new Exception(message: Strings.MainStrings.ValidationFailed);
                    }

                    var htmlDoc = services.GetRequiredService<Scraper>().Scrape(args[0]);
                    var result = services.GetRequiredService<Parser>().Parse(htmlDoc);

                    await services.GetRequiredService<FileSaver>().Save(result.Item1, result.Item2);
                }
            } catch (Exception ex) {
                Output.Error(ex.Message);
            }
        }
    }
}
