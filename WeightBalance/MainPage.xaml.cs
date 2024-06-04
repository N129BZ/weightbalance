using Syncfusion.Maui.ListView;
using System.Collections.ObjectModel;
using WeightBalance.Data;
using WeightBalance.Models;
using wbGlobals = WeightBalance.Data.WbGlobals;


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
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        if (!wbGlobals.PreSelectDone)
        {
            SearchForDefault();
        }
    }

    private void SearchForDefault()
    {
        bool defaultFound = false;

        foreach (Aircraft aircraft in HangarList)
        {
            if (aircraft.IsDefault)
            {   
                this.SelectedAircraft = aircraft;
                AircraftListView.SelectedItem = aircraft;
                defaultFound = true;

                if (aircraft.AutoLoad && !wbGlobals.PreSelectDone)
                {
                    wbGlobals.PreSelectDone = true;
                    ViewStations_Clicked(aircraft, new EventArgs());
                }
            }
        }

        if (!defaultFound)
        {
            SelectedAircraft = HangarList[0];
            AircraftListView.SelectedItem = SelectedAircraft;
        }
    }

    private void ViewStations_Clicked(object sender, EventArgs e)
    {
        IsBusy = true;
        var cgp = new CgPage(SelectedAircraft);
        Navigation.PushAsync(cgp, true);
        IsBusy = false;
    }

    private void SetDefault_Clicked(object sender, EventArgs e)
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
        DisplayAlert("Default Aircraft", $"{SelectedAircraft.Name} is now the default aircraft!", "OK");
    }

    private void AircraftListView_ItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
    {
        SelectedAircraft = (Aircraft)e.DataItem;
    }

    private void EditLimits_Clicked(object sender, EventArgs e)
    {
        var eap = new EditAircraftPage(SelectedAircraft);
        Navigation.PushAsync(eap, true);
    }
}
