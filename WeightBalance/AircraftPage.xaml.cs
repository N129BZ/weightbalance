using WeightBalance.Drawables;
using WeightBalance.Models;

namespace WeightBalance
{
    public partial class AircraftPage : ContentPage
    {
        private readonly string pagetitle = String.Empty;
        public string PageTitle { get { return pagetitle; } }

        private Aircraft? aircraft;
        public Aircraft? SelectedAircraft
        {
            get { return aircraft; }
            set { aircraft = value; }
        }

        public AircraftPage(Aircraft selectedAircraft)
        {
            aircraft = selectedAircraft;
            pagetitle = $"GROSS WT: {aircraft.TotalWeight}, CG: {aircraft.CoG}";

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
                GraphicsView acoverlay = new ()
                {
                    Drawable = new AircraftOverlay { SelectedAircraft = aircraft }
                };

                ImageSource chimgsrc = ImageSource.FromResource(aircraft.ChartResourcePath);
                Image chimage = new()
                {
                    Source = chimgsrc,
                    Aspect = Aspect.AspectFit
                };

                GraphicsView chartoverlay = new()
                {
                    Drawable = new ChartOverlay { SelectedAircraft = aircraft }
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
            Application.Current?.Quit();
        }

        private async void ViewStations_Clicked(object? sender, EventArgs e)
        {
            await Task.Run(() => Navigation.PopAsync(true));
        }

        private async void ViewAircraftSelection_Clicked(object? sender, EventArgs e)
        {
            await Task.Run(() => Navigation.PopToRootAsync(true));
        }
    }
}