using System.Text;
using ImageMagick;
using SAGG.Models;
using SAGG.Modules;

namespace SAGG.Services {
	internal class FileSaver:IDisposable {
		private readonly string CurrentPath = $"{AppDomain.CurrentDomain.BaseDirectory}";
		private string GameSavePath = String.Empty;

		internal async Task Save(string gameName, List<Achievements> achievements) {
			try {
				Output.Info(Strings.FileSaveStrings.SavingStart);
				gameName = gameName.Replace("/", "_").Replace("\\", "_");
				gameName = string.Join("_", gameName.Split(Path.GetInvalidPathChars()));
				GameSavePath = $"{CurrentPath}{Path.DirectorySeparatorChar}{gameName}";

				CreateFolder(achievements);
				await SaveAchievementInfo(achievements);
				await DownloadIcon(achievements);

				Output.Success(Strings.FileSaveStrings.SavingDone);
			} catch (Exception) {
				Output.Error(Strings.FileSaveStrings.SavingError);
				throw;
			} finally {
				Dispose();
			}
		}

		private async Task DownloadIcon(List<Achievements> achievements) {
			try {
				Output.Info(Strings.FileSaveStrings.DownloadStart);

				using var client = new HttpClient();
				var size = new MagickGeometry(64, 64);
				//size.IgnoreAspectRatio = false;

				foreach (var achievement in achievements) {
					var uriWithoutQuery = achievement.IconUri.GetLeftPart(UriPartial.Path);
					var fileExtension = Path.GetExtension(uriWithoutQuery);

					var iconPath1 = $"{GameSavePath}{Path.DirectorySeparatorChar}{string.Join("_", achievement.Name.Split(Path.GetInvalidPathChars())).Replace(".", "_")}{Path.DirectorySeparatorChar}{string.Join("_", achievement.Name.Split(Path.GetInvalidFileNameChars())).Replace(".", "_")}{fileExtension}";
					var iconPath2 = $"{GameSavePath}{Path.DirectorySeparatorChar}IconsFolder{Path.DirectorySeparatorChar}{string.Join("_", achievement.Name.Split(Path.GetInvalidFileNameChars())).Replace(".", "_")}{fileExtension}";

					Output.InfoR(Strings.FileSaveStrings.ClearCurrentLine);
					Output.InfoR($"\rDownloading {achievement.Name}{fileExtension}");
					var imageBytes = await client.GetByteArrayAsync(achievement.IconUri);
					//await File.WriteAllBytesAsync(iconPath, imageBytes);

					// Resizing
					using var image = new MagickImage(imageBytes);

					image.Resize(size);
					await image.WriteAsync(iconPath1);
					await image.WriteAsync(iconPath2);
				}

				Output.Success(string.Empty);
				Output.Success(Strings.FileSaveStrings.DownloadDone);
			} catch (Exception) {
				Output.Error(Strings.FileSaveStrings.DownloadError);
				throw;
			}
		}

		private async Task SaveAchievementInfo(List<Achievements> achievements) {
			try {
				Output.Info(Strings.FileSaveStrings.SavingInfoStart);

				File.Create($"{GameSavePath}{Path.DirectorySeparatorChar}All counted {achievements.Count} achievements.txt");

				StringBuilder sb = new StringBuilder();
				foreach (var achievement in achievements) {
					sb.Clear();
					sb.Append($"{achievement.Name}{Environment.NewLine}");
					sb.Append($"{achievement.Description}{Environment.NewLine}");
					await File.AppendAllTextAsync($"{GameSavePath}{Path.DirectorySeparatorChar}{string.Join("_", achievement.Name.Split(Path.GetInvalidPathChars())).Replace(".", "_")}{Path.DirectorySeparatorChar}{string.Join("_", achievement.Name.Split(Path.GetInvalidFileNameChars())).Replace(".", "_")}.txt", sb.ToString());
				}

				Output.Success(Strings.FileSaveStrings.SavingInfoDone);
			} catch (Exception) {
				Output.Error(Strings.FileSaveStrings.SavingInfoError);
				throw;
			}
 		}

		private void CreateFolder(List<Achievements> achievements) {
			try {
				Output.Info(Strings.FileSaveStrings.CreateFolderStart);

				Directory.CreateDirectory(GameSavePath);
				Directory.CreateDirectory($"{GameSavePath}{Path.DirectorySeparatorChar}IconsFolder");

				foreach (var achievement in achievements) {
					Directory.CreateDirectory($"{GameSavePath}{Path.DirectorySeparatorChar}{string.Join("_", achievement.Name.Split(Path.GetInvalidPathChars())).Replace(".", "_")}");
				}

				Output.Success(Strings.FileSaveStrings.CreateFolderDone);
			} catch (Exception) {
				Output.Error(Strings.FileSaveStrings.CreateFolderError);
				throw;
			}
		}

		public void Dispose() { 
			GC.SuppressFinalize(this);
		}
	}
}
