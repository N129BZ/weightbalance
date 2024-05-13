using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WeightBalance.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace WeightBalance.Data
{
    public static class Hangar
    {
        
        public static ObservableCollection<Aircraft> LoadTheHangar()
        {
            var filePath = Path.Combine(FileSystem.AppDataDirectory, "Aircraft.json");
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<ObservableCollection<Aircraft>>(json)!;
        }

        public static bool SaveTheHangar(ObservableCollection<Aircraft> hangar)
        {
            try
            {
                var filepath = Path.Combine(FileSystem.AppDataDirectory, "Aircraft.json");
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
