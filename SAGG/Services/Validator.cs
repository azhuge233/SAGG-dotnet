using SAGG.Modules;
using SAGG.Models;

namespace SAGG.Services {
	internal class Validator : IDisposable {
		internal bool Validate(string[] args) {
			try {
				if(args.Length != 1) return false;

				string appID = args[0];
				if (string.IsNullOrWhiteSpace(appID) || !appID.All(char.IsDigit)) return false;

				Output.Success(Strings.ValidateStrings.ValidateInfo);
				return true;
			} catch (Exception) {
				Output.Error(Strings.ValidateStrings.ValidateError);
				throw;
			}
		}

		internal bool Validate(Config config) {
			try {
				if (string.IsNullOrEmpty(config.Token) || string.IsNullOrEmpty(config.PreferedLanguage)) return false;

				Output.Success(Strings.ValidateStrings.ValidateInfo);
				return true;
			} catch (Exception) {
				Output.Error(Strings.ValidateStrings.ValidateError);
				throw;
			}
		}

		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
