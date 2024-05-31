using System.Runtime.InteropServices;
using WeightBalance.Drawables;
using WeightBalance.Models;

namespace WeightBalance;

public partial class ChartPage : ContentPage
{
    private readonly string pagetitle = String.Empty;
    private readonly double CoG;
    public string PageTitle { get { return pagetitle; } }

    private Aircraft? aircraft;
    public Aircraft? SelectedAircraft
    {
        get { return aircraft; }
        set { aircraft = value; }
    }

    
    
    public ChartPage(Aircraft selectedAircraft, double cog)
    {
        aircraft = selectedAircraft;
        CoG = cog;

        pagetitle = $"WT: {aircraft.TotalWeight}, CG: {CoG}";

        InitializeComponent();

        if (aircraft == null)
        {
            throw new Exception("selectedAircraft cannot be null!");
        }
        else
        {
            Grid view = PageGrid;

            ImageSource acimgsrc = ImageSource.FromResource(aircraft.AircraftResourcePath);
            Image acimage = new()
            {
                Source = acimgsrc,
                Aspect = Aspect.AspectFit
            };
            GraphicsView acoverlay = new()
            {
                Drawable = new AircraftOverlay(aircraft, CoG)
            };

            ImageSource chimgsrc = ImageSource.FromResource(aircraft.ChartResourcePath);
            Image chimage = new()
            {
                Source = chimgsrc,
                Aspect = Aspect.AspectFit
            };

            GraphicsView chartoverlay = new()
            {
                Drawable = new ChartOverlay(aircraft, CoG)
            };

            view.Add(acimage, 0, 0);
            view.Add(acoverlay, 0, 0);
            view.Add(chimage, 0, 1);
            view.Add(chartoverlay, 0, 1);

            Content = view;
        }

        BindingContext = this;
    }

    private void ExitHangar_Clicked(object? sender, EventArgs e)
    {
#if IOS
        Globals.exit(0);
#else
        Application.Current?.Quit();
#endif
    }

    private async void ViewStations_Clicked(object? sender, EventArgs e)
    {
        await Navigation.PopAsync(true);
    }

    private async void ViewAircraftSelection_Clicked(object? sender, EventArgs e)
    {
        await Navigation.PopToRootAsync(true);
    }
}