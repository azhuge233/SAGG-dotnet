namespace SAGG.Modules {
	public static class Output {

		public static void ResetColor() {
			Console.ResetColor();
		}
		public static void Error(string msg) { 
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(msg);
			ResetColor();
		}
		public static void Warning(string msg) {
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(msg);
			ResetColor();
		}
		public static void Info(string msg) {
			Console.WriteLine(msg);
			ResetColor();
		}
	}
}
