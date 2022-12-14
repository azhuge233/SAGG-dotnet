using Microsoft.Extensions.DependencyInjection;
using SAGG.Modules;
using SAGG.Services;

namespace SAGG {
    class Program {
         static async Task Main(string[] args) {
            try {
                var services = DIBuilder.Build();

                using (services as IDisposable) {
                    var config = services.GetRequiredService<JsonLoader>().LoadConfig();
                    var validator = services.GetRequiredService<Validator>();

                    if (!validator.Validate(config))
                        throw new Exception(message: Strings.MainStrings.ConfigValidationFailed);

					if (!validator.Validate(args))
                        throw new Exception(message: Strings.MainStrings.AppIDValidationFailed);

                    var jsonData = await services.GetRequiredService<Scraper>().Scrape(config, args[0]);
                    var result = services.GetRequiredService<Parser>().Parse(jsonData);

                    await services.GetRequiredService<FileSaver>().Save(result.Item1, result.Item2);
                }
            } catch (Exception ex) {
                Output.Error(ex.Message);
            }
        }
    }
}
