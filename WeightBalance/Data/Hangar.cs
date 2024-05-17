using System.Collections.ObjectModel;
using System.Text.Json;
using WeightBalance.Models;

namespace WeightBalance.Data;

public class Hangar
{
    public Hangar()
    {
        LoadTheHangarList();
    }

    private ObservableCollection<Aircraft> hangarList;
    public ObservableCollection<Aircraft> HangarList { get { return hangarList; } }

    public void LoadTheHangarList()
    {
        var filePath = Path.Combine(FileSystem.Current.AppDataDirectory, "aircraft.json");
        var json = File.ReadAllText(filePath);
        hangarList = JsonSerializer.Deserialize<ObservableCollection<Aircraft>>(json)!;
    }

    public bool SaveHangarList()
    {
        try
        {
            var filepath = Path.Combine(FileSystem.AppDataDirectory, "aircraft.json");
            var output = JsonSerializer.Serialize(hangarList);
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
