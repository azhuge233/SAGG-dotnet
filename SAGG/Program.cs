using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SAGG.Modules;
using SAGG.Services;

namespace SAGG {
    class Program {
         static void Main(string[] args) {
            try {
                var services = DIBuilder.Build();

                using (services as IDisposable) {
                    if (!services.GetRequiredService<Validator>().Validate(args)) {
                        throw new Exception(message: Strings.MainStrings.ValidationFailed);
                    }

                    services.GetRequiredService<Scraper>().Scrape(args[0]);
                    
                }
            } catch (Exception ex) {
                Output.Error(ex.Message);
            }
        }
    }
}
