using System.Collections.ObjectModel;
using System.Text.Json;
using WeightBalance.Models;

namespace WeightBalance.Data;

public class Hangar
{
    public Hangar()
    {
        LoadHangarList();
    }

    public ObservableCollection<Aircraft> HangarList { get; private set; } = [];

    public void LoadHangarList()
    {
        var filePath = Path.Combine(FileSystem.Current.AppDataDirectory, "aircraft.json");
        var json = File.ReadAllText(filePath);
        HangarList = JsonSerializer.Deserialize<ObservableCollection<Aircraft>>(json)!;
    }

    public bool SaveHangarList()
    {
        try
        {
            var filepath = Path.Combine(FileSystem.AppDataDirectory, "aircraft.json");
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
