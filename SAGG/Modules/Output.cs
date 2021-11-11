namespace SAGG.Modules {
	internal static class Output {

		internal static void ResetColor() {
			Console.ResetColor();
		}
		internal static void Error(string msg) { 
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(msg);
			ResetColor();
		}
		internal static void Success(string msg) {
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine(msg);
			ResetColor();
		}
		internal static void Info(string msg) {
			Console.WriteLine(msg);
		}
		internal static void InfoR(string msg) {
			Console.Write(msg);
		}
	}
}
