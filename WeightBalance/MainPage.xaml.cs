using Syncfusion.Maui.ListView;
using System.Collections.ObjectModel;
using WeightBalance.Data;
using WeightBalance.Models;


namespace WeightBalance
{
    public partial class MainPage : ContentPage
    {
        private readonly Hangar hangar = new();
        
        private readonly ObservableCollection<Aircraft>? hangarList;
        public ObservableCollection<Aircraft>? HangarList { get { return hangarList; } }

        private Aircraft aircraft = new();
        public Aircraft SelectedAircraft { get { return aircraft; } set { aircraft = value; } }

        public MainPage()
        {
            InitializeComponent();

            hangarList = hangar.HangarList;

            SearchForDefault();

            BindingContext = this;
        }

        private void SearchForDefault()
        {
            foreach (Aircraft aircraft in hangar.HangarList)
            {
                if (aircraft.IsDefault)
                {   
                    this.aircraft = aircraft;
                    AircraftListView.SelectedItem = aircraft;
                    //Task.Run(() => DoStationNavigation());
                    return;
                }
            }

            // if we got here, there was no default Aircraft,
            // so set _aircraft to the 1st item in the list
            aircraft = hangarList[0];
            AircraftListView.SelectedItem = aircraft;
        }

        private async void ViewStations_Clicked(object sender, EventArgs e)
        {
            var cgp = new CoGPage(aircraft, hangar);
            await Navigation.PushAsync(cgp, true);
        }

        protected override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            SearchForDefault();
            base.OnNavigatedTo(args);
        }
        private void SetDefault_Clicked(object sender, EventArgs e)
        {
            aircraft.IsDefault = true;
            foreach (Aircraft ac in hangarList)
            {
                if (ac.ID != aircraft.ID)
                {
                    ac.IsDefault = false;
                }
            }
            hangar.SaveHangarList(hangarList);
        }

        private void ExitHangar_Clicked(object sender, EventArgs e)
        {
            Application.Current?.Quit();
        }

        private void AircraftList_ItemDoubleTapped(object sender, ItemDoubleTappedEventArgs e)
        {
            aircraft = (Aircraft)e.DataItem;
            SetDefault_Clicked(sender, e);
            ViewStations_Clicked(sender, e);
        }

        private void AircraftListView_ItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
        {
            aircraft = (Aircraft)e.DataItem;
        }
    }
}
