using SAGG.Modules;

namespace SAGG.Services {
	internal class Validator : IDisposable {
		internal bool Validate(string[] args) {
			try {
				if(args.Length != 1) return false;

				string appID = args[0];
				if (string.IsNullOrWhiteSpace(appID) || appID.All(char.IsDigit)) return false;

				Output.Info(Strings.ValidateStrings.ValidateInfo);
				return true;
			} catch (Exception ex) {
				Output.Error(Strings.ValidateStrings.ValidateError);
				Output.Error(ex.Message);
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
