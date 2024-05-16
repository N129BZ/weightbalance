using System.Collections.ObjectModel;
using System.Text.Json;
using WeightBalance.Models;

namespace WeightBalance.Data
{
    public class Hangar
    {
        public Hangar()
        {
            HangarList = LoadTheHangarList();
        }

        private ObservableCollection<Aircraft> _hangarList;
        public ObservableCollection<Aircraft> HangarList
        {
            get
            {
                return _hangarList;
            }
            set
            {
                _hangarList = value;
            }
        }

        public static ObservableCollection<Aircraft> LoadTheHangarList()
        {
            var filePath = Path.Combine(FileSystem.AppDataDirectory, "aircraft.json");
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<ObservableCollection<Aircraft>>(json)!;
        }

        public static bool SaveHangarList(ObservableCollection<Aircraft> hangar)
        {
            try
            {
                var filepath = Path.Combine(FileSystem.AppDataDirectory, "aircraft.json");
                var output = JsonSerializer.Serialize(hangar);
                using StreamWriter outstream = File.CreateText(filepath);
                outstream.Write(output);
                outstream.Flush();
                outstream.Close();
                outstream.Dispose();
                return true;
            }
            catch { return false; }
        }
    }
}
