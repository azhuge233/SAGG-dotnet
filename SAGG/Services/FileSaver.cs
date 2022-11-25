using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using ImageMagick;
using SAGG.Models;
using SAGG.Modules;

namespace SAGG.Services {
	internal class FileSaver:IDisposable {
		private readonly string CurrentPath = $"{Environment.CurrentDirectory}";
		private string GameSavePath = string.Empty;

		internal async Task Save(string gameName, List<Achievement> achievements) {
			try {
				Output.Info(Strings.FileSaveStrings.SavingStart);
				gameName = gameName.Replace("/", "_").Replace("\\", "_").Replace(":", "_");
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

		private async Task DownloadIcon(List<Achievement> achievements) {
			try {
				Output.Info(Strings.FileSaveStrings.DownloadStart);

				using var client = new HttpClient();
				var size = new MagickGeometry(64, 64);
				//size.IgnoreAspectRatio = false;

				foreach (var achievement in achievements) {
					var uriWithoutQuery = achievement.IconUri.GetLeftPart(UriPartial.Path);
					var fileExtension = Path.GetExtension(uriWithoutQuery);

					var iconPath1 = @$"{GetDirectory(achievement)}{Path.DirectorySeparatorChar}{GetFileName(achievement)}{fileExtension}";
					var iconPath2 = @$"{GameSavePath}{Path.DirectorySeparatorChar}“0. All Icons{Path.DirectorySeparatorChar}{GetFileName(achievement)}{fileExtension}";

					Output.InfoR(Strings.FileSaveStrings.ClearCurrentLine);
					Output.InfoR($"\rDownloading {achievement.DisplayName}{fileExtension}");
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

		private async Task SaveAchievementInfo(List<Achievement> achievements) {
			try {
				Output.Info(Strings.FileSaveStrings.SavingInfoStart);

				File.Create(@$"{GameSavePath}{Path.DirectorySeparatorChar}All counted {achievements.Count} achievements");

				StringBuilder sb = new();
				foreach (var achievement in achievements) {
					sb.Clear();
					sb.Append($"[b]{achievement.DisplayName}[/b]{Environment.NewLine}");
					sb.Append($"[i]{achievement.Description}[/i]{Environment.NewLine}");
					await File.AppendAllTextAsync(@$"{GetDirectory(achievement)}{Path.DirectorySeparatorChar}{GetFileName(achievement)}.txt", sb.ToString());
				}

				Output.Success(Strings.FileSaveStrings.SavingInfoDone);
			} catch (Exception) {
				Output.Error(Strings.FileSaveStrings.SavingInfoError);
				throw;
			}
 		}

		private void CreateFolder(List<Achievement> achievements) {
			try {
				Output.Info(Strings.FileSaveStrings.CreateFolderStart);

				Directory.CreateDirectory(GameSavePath);
				Directory.CreateDirectory(@$"{GameSavePath}{Path.DirectorySeparatorChar}“0. All Icons");

				foreach (var achievement in achievements) {
					var directory = GetDirectory(achievement);
					Directory.CreateDirectory(directory);
				}

				Output.Success(Strings.FileSaveStrings.CreateFolderDone);
			} catch (Exception) {
				Output.Error(Strings.FileSaveStrings.CreateFolderError);
				throw;
			}
		}

		private string GetDirectory(Achievement achievement) { 
			return achievement.Hidden == 0 ? @$"{GameSavePath}{Path.DirectorySeparatorChar}{GetFileName(achievement)}" :
				@$"{GameSavePath}{Path.DirectorySeparatorChar}“1. Hidden Achievements{Path.DirectorySeparatorChar}{GetFileName(achievement)}";
		}

		private static string GetFileName(Achievement achievement) {
			return string.Join("_", achievement.DisplayName.Split(Path.GetInvalidFileNameChars())).Replace(".", "_").Replace("?", "_");
		}

		public void Dispose() { 
			GC.SuppressFinalize(this);
		}
	}
}
