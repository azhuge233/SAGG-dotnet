﻿using Microsoft.Extensions.DependencyInjection;
using SAGG.Modules;
using SAGG.Services;

namespace SAGG {
    class Program {
         static async Task Main(string[] args) {
            try {
                var services = DIBuilder.Build();

                using (services as IDisposable) {
                    var config = services.GetRequiredService<JsonLoader>().LoadConfig();

                    if (!services.GetRequiredService<Validator>().Validate(config, args)) {
                        throw new Exception(message: Strings.MainStrings.ValidationFailed);
                    }

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
