using Syncfusion.Maui.DataSource.Extensions;
using System.Collections.ObjectModel;
using System.Text.Json;
using WeightBalance.Models;
using WeightBalance.Data;
using Microsoft.Maui.Controls;

namespace WeightBalance
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<Aircraft> _thehangar = new();
        public ObservableCollection<Aircraft> TheHangar { get { return _thehangar; } set { _thehangar = value; } }

        private Aircraft _aircraft = new();
        public Aircraft SelectedAircraft { get { return _aircraft; } set { _aircraft = value; } }

        public MainPage()
        {
            InitializeComponent();

            
            
            _thehangar = Hangar.GetTheHangar();
            
            foreach (Aircraft ac in _thehangar)
            {
                if (ac.IsDefault) 
                { 
                    _aircraft = ac;
                    AircraftList.SelectedItem = ac;
                    Task.Run(() => DoStationNavigation());
                    break;
                }
            }

            BindingContext = this;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            _aircraft = (Aircraft)e.Item;
        }

        private void DoStationNavigation()
        {
            Object o = new Object();
            EventArgs e = new EventArgs();
            ViewStations_Clicked(o, e);
        }
        private void ViewStations_Clicked(object sender, EventArgs e)
        {
            var cgp = new CoGPage(_aircraft, _thehangar);
            Navigation.PushAsync(cgp);
        }

        private void SetDefault_Clicked(object sender, EventArgs e)
        {
            foreach (Aircraft ac in _thehangar)
            {
                if (ac.ID != _aircraft.ID)
                {
                    ac.IsDefault = false;
                }
            }
            _aircraft.IsDefault = Hangar.SaveTheHangar(_thehangar);
            
            ViewStations_Clicked(this, new EventArgs());
        }

        private void ExitHangar_Clicked(object sender, EventArgs e)
        {
            Application.Current.Quit();
        }
    }
}
