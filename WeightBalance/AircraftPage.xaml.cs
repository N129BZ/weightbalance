using WeightBalance.Drawables;
using WeightBalance.Models;

namespace WeightBalance
{
    public partial class AircraftPage : ContentPage
    {
        private string _pagetitle = String.Empty;
        public string PageTitle { get { return _pagetitle; } set { _pagetitle = value; } }

        public AircraftPage(Aircraft aircraft)
        {
            _pagetitle = $"GROSS WT: {aircraft.TotalWeight}, CG: {aircraft.CoG}";

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

        private void ViewStations_Clicked(object? sender, EventArgs e)
        {
            Shell.Current.Navigation.PopAsync();
        }

        private void ViewAircraftSelection_Clicked(object? sender, EventArgs e)
        {
            Shell.Current.Navigation.PopToRootAsync();
        }
    }
}