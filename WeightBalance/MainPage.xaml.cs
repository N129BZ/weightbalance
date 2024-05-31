using Syncfusion.Maui.ListView;
using System.Collections.ObjectModel;
using WeightBalance.Data;
using WeightBalance.Models;


namespace WeightBalance;

public partial class MainPage : ContentPage
{
    private readonly Hangar hangar = new();
    
    public ObservableCollection<Aircraft> HangarList { get; private set; } = [];

    public Aircraft SelectedAircraft { get; private set; } = new();

    private Command tapcommand;
    public Command TapCommand
    {
        get { return tapcommand; }
        private set { tapcommand = value; }
    }

    public MainPage()
    {
        InitializeComponent();

        HangarList = hangar.HangarList;

        tapcommand = new Command(EditLimits_Clicked);

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
                if (Globals.IsFirstRun)
                {
                    Globals.IsFirstRun = false;
                    ViewStations_Clicked(this, new EventArgs());
                    return;
                }
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
        var cgp = new CgPage(SelectedAircraft, hangar);
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
        bool answer = await DisplayAlert("Set Default Aircraft", 
            "When you set a default aircraft, the application will automatically go to the Stations view when launched. Continue?", 
            "Yes", "No");
        if (answer)
        {
            SelectedAircraft.IsDefault = true;
            foreach (Aircraft ac in HangarList)
            {
                if (ac.ID != SelectedAircraft.ID)
                {
                    ac.IsDefault = false;
                }
            }
            hangar.SaveHangarList();
        }
    }

    private void ExitHangar_Clicked(object sender, EventArgs e)
    {
        Application.Current?.Quit();
    }

    private void AircraftList_ItemDoubleTapped(object sender, ItemDoubleTappedEventArgs e)
    {
        SelectedAircraft = (Aircraft)e.DataItem;
        SetDefault_Clicked(sender, e);
        ViewStations_Clicked(sender, e);
    }

    private void AircraftListView_ItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
    {
        SelectedAircraft = (Aircraft)e.DataItem;
    }

    private async void EditLimits_Clicked(object sender)
    {
        Aircraft editaircraft = sender as Aircraft;
        var eap = new EditAircraftPage(editaircraft, hangar);
        await Navigation.PushAsync(eap, true);
    }
}
