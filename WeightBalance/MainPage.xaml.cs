using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace WeightBalance
{ 
    
    public partial class MainPage : ContentPage
    {
        public bool IsRefreshing { get; set; }
        public ObservableCollection<Aircraft>? TheHangar { get; set; }
        public Aircraft? SelectedAircraft { get; set; }

        public MainPage()
        {
            InitializeComponent();

            TheHangar = Task.Run(() => LoadAircraft()).Result;
            BindingContext = this;
        }

        public async Task<ObservableCollection<Aircraft>> LoadAircraft ()
        {
            var filePath = Path.Combine(FileSystem.AppDataDirectory, "Aircraft.json");
        
            if (!File.Exists(filePath))
            {
                await CopyAircraftJsonToAppDataDirectory();
            }
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<ObservableCollection<Aircraft>>(json)!;
        }

        private async Task CopyAircraftJsonToAppDataDirectory()
        {
            var fpath = Path.Combine(FileSystem.AppDataDirectory, "Aircraft.json");
            using var inputStream = await FileSystem.OpenAppPackageFileAsync("Aircraft.json");
            using var outputStream = File.Create(fpath);
            await inputStream.CopyToAsync(outputStream);

        }

        private void AircraftSelected(object sender, EventArgs e)
        {
            Console.WriteLine(e.ToString());
        }

        private void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            AircraftPage ap = new AircraftPage((Aircraft)e.Item);
            Navigation.PushAsync(ap);
        }
    }
}
