using WeightBalance.Drawables;
using WeightBalance.Models;

namespace WeightBalance
{
    public partial class AircraftPage : ContentPage
    {
        private string _pagetitle = String.Empty;
        public string PageTitle { get { return _pagetitle; } set { _pagetitle = value; } }

        private Aircraft? _aircraft;
        public Aircraft? SelectedAircraft
        {
            get { return _aircraft; }
            set { _aircraft = value; }
        }

        public AircraftPage(Aircraft selectedAircraft)
        {
            _aircraft = selectedAircraft;
            _pagetitle = $"GROSS WT: {_aircraft.TotalWeight}, CG: {_aircraft.CoG}";

            InitializeComponent();

            if (_aircraft == null)
            {
                throw new Exception("selectedAircraft cannot be null!");
            }
            else
            {
                Grid view = PageGrid;

                ImageSource acimgsrc = ImageSource.FromResource(_aircraft.AircraftResourcePath);
                Image acimage = new()
                {
                    Source = acimgsrc,
                    Aspect = Aspect.AspectFit
                };
                GraphicsView acoverlay = new ()
                {
                    Drawable = new AircraftOverlay { SelectedAircraft = _aircraft }
                };

                ImageSource chimgsrc = ImageSource.FromResource(_aircraft.ChartResourcePath);
                Image chimage = new()
                {
                    Source = chimgsrc,
                    Aspect = Aspect.AspectFit
                };

                GraphicsView chartoverlay = new()
                {
                    Drawable = new ChartOverlay { SelectedAircraft = _aircraft }
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