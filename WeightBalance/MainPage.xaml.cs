using Syncfusion.Maui.ListView;
using System.Collections.ObjectModel;
using WeightBalance.Data;
using WeightBalance.Models;


namespace WeightBalance
{
    public partial class MainPage : ContentPage
    {
        private readonly Hangar _hangar = new();
        
        private ObservableCollection<Aircraft> _hangarList = [];
        public ObservableCollection<Aircraft> HangarList { get { return _hangarList; } set { _hangarList = value; } }

        private Aircraft _aircraft = new();
        public Aircraft SelectedAircraft { get { return _aircraft; } set { _aircraft = value; } }

        public MainPage()
        {
            InitializeComponent();

            _hangarList = _hangar.HangarList;

            SearchForDefault();

            BindingContext = this;
        }

        private void SearchForDefault()
        {
            foreach (Aircraft aircraft in _hangar.HangarList)
            {
                if (aircraft.IsDefault)
                {   
                    _aircraft = aircraft;
                    AircraftListView.SelectedItem = aircraft;
                    //Task.Run(() => DoStationNavigation());
                    return;
                }
            }

            // if we got here, there was no default Aircraft,
            // so set _aircraft to the 1st item in the list
            _aircraft = _hangarList[0];
            AircraftListView.SelectedItem = _aircraft;
        }

        private async void ViewStations_Clicked(object sender, EventArgs e)
        {
            var cgp = new CoGPage(_aircraft, _hangar);
            await Navigation.PushAsync(cgp, true);
        }

        protected override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            SearchForDefault();
            base.OnNavigatedTo(args);
        }
        private void SetDefault_Clicked(object sender, EventArgs e)
        {
            _aircraft.IsDefault = true;
            foreach (Aircraft ac in _hangarList)
            {
                if (ac.ID != _aircraft.ID)
                {
                    ac.IsDefault = false;
                }
            }
            _hangar.SaveHangarList(_hangarList);
        }

        private void ExitHangar_Clicked(object sender, EventArgs e)
        {
            Application.Current?.Quit();
        }

        private void AircraftList_ItemDoubleTapped(object sender, ItemDoubleTappedEventArgs e)
        {
            _aircraft = (Aircraft)e.DataItem;
            SetDefault_Clicked(sender, e);
            ViewStations_Clicked(sender, e);
        }

        private void AircraftListView_ItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
        {
            _aircraft = (Aircraft)e.DataItem;
        }
    }
}
