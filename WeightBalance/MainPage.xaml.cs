using Syncfusion.Maui.ListView;
using System.Collections.ObjectModel;
using WeightBalance.Data;
using WeightBalance.Models;


namespace WeightBalance;

public partial class MainPage : ContentPage
{
    public Aircraft SelectedAircraft { get; private set; } = new();
    
    public ObservableCollection<Aircraft> HangarList { get; private set; }

    public MainPage()
    {
        InitializeComponent();
        
        Hangar.LoadHangarList();

        HangarList = Hangar.HangarList;
        
        BindingContext = this;
        
        SearchForDefault();
    }

    private void SearchForDefault()
    {
        foreach (Aircraft aircraft in HangarList)
        {
            if (aircraft.IsDefault)
            {   
                this.SelectedAircraft = aircraft;
                AircraftListView.SelectedItem = aircraft;
                return;
            }
        }

        // if we got here, there was no default Aircraft,
        // so set _aircraft to the 1st item in the list
        SelectedAircraft = HangarList[0];
        AircraftListView.SelectedItem = SelectedAircraft;
    }

    private async void ViewStations_Clicked(object sender, EventArgs e)
    {
        IsBusy = true;
        var cgp = new CgPage(SelectedAircraft);
        await Navigation.PushAsync(cgp, true);
        IsBusy = false;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        SearchForDefault();
        base.OnNavigatedTo(args);
    }
    private async void SetDefault_Clicked(object sender, EventArgs e)
    {
        SelectedAircraft.IsDefault = true;
        foreach (Aircraft ac in HangarList)
        {
            if (ac.ID != SelectedAircraft.ID)
            {
                ac.IsDefault = false;
            }
        }
        Hangar.SaveHangarList();

        await DisplayAlert("Default Aircraft", "The default aircraft has been set!", "OK");

    }

    private void AircraftListView_ItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
    {
        SelectedAircraft = (Aircraft)e.DataItem;
    }

    private async void EditLimits_Clicked(object sender, EventArgs e)
    {
        var eap = new EditAircraftPage(SelectedAircraft);
        await Navigation.PushAsync(eap, true);
    }
}
