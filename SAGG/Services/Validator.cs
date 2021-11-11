using SAGG.Modules;

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
			} finally {
				Dispose();
			}
		}

		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
