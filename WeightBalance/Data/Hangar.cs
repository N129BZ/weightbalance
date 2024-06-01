using System.Collections.ObjectModel;
using System.Text.Json;
using WeightBalance.Models;

namespace WeightBalance.Data;

public static class Hangar
{
    //public Hangar()
    //{
    //    LoadHangarList();
    //}

    public static ObservableCollection<Aircraft> HangarList { get; private set; } = [];

    public static void LoadHangarList()
    {
        var filePath = Path.Combine(FileSystem.Current.AppDataDirectory, "aircraft.json");
        var json = File.ReadAllText(filePath);
        HangarList = JsonSerializer.Deserialize<ObservableCollection<Aircraft>>(json)!;
    }

    public static bool SaveHangarList()
    {
        try
        {
            var filepath = Path.Combine(FileSystem.Current.AppDataDirectory, "aircraft.json");
            var output = JsonSerializer.Serialize(HangarList);
            using StreamWriter outstream = File.CreateText(filepath); 
            outstream.Write(output);
            outstream.Flush();
            outstream.Close();
            return true;
        }
        catch { return false; }
    }
}
