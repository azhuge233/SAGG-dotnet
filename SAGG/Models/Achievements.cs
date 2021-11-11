namespace SAGG.Models {
	internal class Achievements {
		internal string Name;
		internal string Description;
		internal Uri IconUri;

		public Achievements(string name, string description, string uri) {
			Name = name;
			Description = description;
			IconUri = new Uri(uri);
		}
	}
}
