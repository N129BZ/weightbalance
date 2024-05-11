using System.Collections.ObjectModel;
using System.Text.Json;
using WeightBalance.Models;

namespace WeightBalance
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<Aircraft>? _thehangar;
        public ObservableCollection<Aircraft>? TheHangar
        {
            get { return _thehangar; }
            set { _thehangar = value; }
        }

        public Aircraft? SelectedAircraft { get; set; }

        public MainPage()
        {
            InitializeComponent();

            _thehangar = LoadAircraft();

            BindingContext = this;
        }
        
        public ObservableCollection<Aircraft> LoadAircraft()
        {
            var filePath = Path.Combine(FileSystem.AppDataDirectory, "Aircraft.json");
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<ObservableCollection<Aircraft>>(json)!;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var aircraft = (Aircraft)e.Item;
            CoGPage cog = new CoGPage(aircraft);
            Navigation.PushAsync(cog);
        }
    }
}
