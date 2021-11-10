using System.Collections.Generic;

namespace SAGG.Strings {
	public static class ParseStrings {
		public static readonly List<string> ForbidWordlist = new() { "<", ">", "|", ":", "?", "/", "\"", "*"};
	}
}
