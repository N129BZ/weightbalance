using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml;

namespace WeightBalance
{
    //public class CoGUnit
    //{
    //    public string Name { get; set; }
    //    public float Weight { get; set; }
    //    public float Arm {  get; set; }

    //    [property:JsonIgnore]
    //    public float Moment
    //    {
    //        get
    //        {
    //            return Weight * Arm;
    //        }
    //    }
    //}

    //internal class AircraftCoGUnits
    //{
    //    public ObservableCollection<CoGUnit>? CoGUnits { get; set; }
    //    public Aircraft? SelectedAircraft { get; set; }
    //    public AircraftCoGUnits(Aircraft? selectedAircraft) 
    //    {
    //        SelectedAircraft = selectedAircraft;
    //        CoGUnits = new ObservableCollection<CoGUnit>();
    //        CoGUnits.Add(new CoGUnit("LeftMain", 0, 0));
    //        CoGUnits.Add(new CoGUnit("RightMain", 0, 0));
    //        CoGUnits.Add(new CoGUnit("NoseWheel", 0, 0));
    //        CoGUnits.Add(new CoGUnit("Pilot", 0, 0));
    //        CoGUnits.Add(new CoGUnit("Passenger", 0, 0));
    //        CoGUnits.Add(new CoGUnit("Fuel", 0, 0));
    //        CoGUnits.Add(new CoGUnit("LeftWingLocker", 0, 0));
    //        CoGUnits.Add(new CoGUnit("RightWingLocker", 0, 0));
    //        CoGUnits.Add(new CoGUnit("Baggage", 0, 0));
    //        SaveCoGUnits();
    //    }

    //    public void SaveCoGUnits()
    //    {
    //        var filePath = Path.Combine(FileSystem.AppDataDirectory, $"{SelectedAircraft.Name}_CoGUnits.json");
    //        if (!File.Exists(filePath))
    //        {
    //            var json = JsonSerializer.Serialize(CoGUnits, new JsonSerializerOptions { WriteIndented = true });
    //            File.WriteAllText(filePath, json);
    //        }

    //        //string json = File.ReadAllText(filePath);
    //        //return JsonSerializer.Deserialize<ObservableCollection<CoGUnit>>(json)!;
    //    }

        //public async Task<ObservableCollection<CoGUnit>> LoadCoGUnits()
        //{
        //    var filePath = Path.Combine(FileSystem.AppDataDirectory, "CoGUnits.json");
        //    if (!File.Exists(filePath))
        //    {
        //        await CopyCoGUnitsJsonToAppDataDirectory();
        //    }

        //    string json = File.ReadAllText(filePath);
        //    return JsonSerializer.Deserialize<ObservableCollection<CoGUnit>>(json)!;
        //}

        //private async Task CopyCoGUnitsJsonToAppDataDirectory()
        //{
        //    var fpath = Path.Combine(FileSystem.AppDataDirectory, "CoGUnits.json");
        //    using var inputStream = await FileSystem.OpenAppPackageFileAsync("CoGUnits.json");
        //    using var outputStream = File.Create(fpath);
        //    await inputStream.CopyToAsync(outputStream);
        //}

    //}
}
