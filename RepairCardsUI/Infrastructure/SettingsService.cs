using Newtonsoft.Json;
using System;
using System.IO;

namespace RepairCardsUI.Infrastructure
{
    public class SettingsService
	{
		private string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RepairCardsSettings.json");

		public void Save(Settings settings)
		{
			File.WriteAllText(_path, JsonConvert.SerializeObject(settings));
		}

		public Settings Load()
		{
			if (File.Exists(_path))
				return JsonConvert.DeserializeObject<Settings>(File.ReadAllText(_path));
			else 
				return new Settings();
		}
	}
}
