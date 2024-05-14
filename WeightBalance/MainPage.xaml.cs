using System.Collections.ObjectModel;
using Syncfusion.Maui.ListView;
using WeightBalance.Models;
using WeightBalance.Data;


namespace WeightBalance
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<Aircraft> _thehangar = [];
        public ObservableCollection<Aircraft> TheHangar { get { return _thehangar; } set { _thehangar = value; } }

        private Aircraft _aircraft = new();
        public Aircraft SelectedAircraft { get { return _aircraft; } set { _aircraft = value; } }

        public MainPage()
        {
            InitializeComponent();
           
            _thehangar = Hangar.LoadTheHangar();

            BindingContext = this;

            SearchForDefault();
        }

        private void SearchForDefault()
        {
            foreach (Aircraft ac in _thehangar)
            {
                if (ac.IsDefault)
                {
                    _aircraft = ac;
                    AircraftListView.SelectedItem = ac;
                    Task.Run(() => DoStationNavigation());
                    return;
                }
            }

            // if we got here, there was no default Aircraft,
            // so set _aircraft to the 1st item in the list
            _aircraft = _thehangar[0];
            AircraftListView.SelectedItem = _aircraft;
        }

        private void DoStationNavigation()
        {
            var e = new EventArgs();
            ViewStations_Clicked(this, e);
        }
        private void ViewStations_Clicked(object sender, EventArgs e)
        {
            var cgp = new CoGPage(_aircraft, _thehangar);
            Navigation.PushAsync(cgp);
        }

        private void SetDefault_Clicked(object sender, EventArgs e)
        {
            _aircraft.IsDefault = true;
            foreach (Aircraft ac in _thehangar)
            {
                if (ac.ID != _aircraft.ID)
                {
                    ac.IsDefault = false;
                }
            }
            Hangar.SaveTheHangar(_thehangar);
        }

        private void ExitHangar_Clicked(object sender, EventArgs e)
        {
            Application.Current?.Quit();
        }

        private void AircraftList_ItemDoubleTapped(object sender, ItemDoubleTappedEventArgs e)
        {
            _aircraft = (Aircraft)e.DataItem;
            SetDefault_Clicked(sender, new EventArgs());
            ViewStations_Clicked(sender, new EventArgs());
        }

        private void AircraftListView_ItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
        {
            _aircraft = (Aircraft)e.DataItem;
        }
    }
}
