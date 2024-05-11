using WeightBalance.Models;
using WeightBalance.Drawables;
using SkiaSharp.Views.Maui.Controls;

namespace WeightBalance
{
    public partial class AircraftPage : ContentPage
    {
        private string _actitle = String.Empty;

        public string AcTitle { get { return _actitle; } }

        public AircraftPage(Aircraft aircraft)
        {
            _actitle = $"{aircraft.Name.ToUpper()} MAX GROSS: {aircraft.MaxGross}";
            
            InitializeComponent();

            if (aircraft == null)
            {
                throw new Exception("selectedAircraft cannot be null!");
            }
            else
            {
                //var page = new AircraftSkiaPage(aircraft);

                Grid views = new Grid();
                views.RowDefinitions = new RowDefinitionCollection
                    {
                        new RowDefinition(new GridLength(220)),
                        new RowDefinition(new GridLength(480)),
                        new RowDefinition(new GridLength(100))
                    };
                //SKCanvasView acchart = new SKCanvasView();

                GraphicsView acchart = new GraphicsView
                {
                    Drawable = new AircraftChart(aircraft) as IDrawable
                };

                views.Add(aircraft.AircraftImage, 0, 0);
                views.Add(acchart, 0, 0);

                GraphicsView mainchart = new GraphicsView
                {
                    Drawable = new MainChart(aircraft) as IDrawable,
                };
                views.Add(mainchart, 0, 1);

                HorizontalStackLayout buttonLayout = new HorizontalStackLayout { HorizontalOptions = LayoutOptions.Center };
                Button StationPageButton = new Button
                {
                    Text = "View Stations",
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    Padding = 6,
                    Margin = 6,
                    WidthRequest = 150
                };
                StationPageButton.Clicked += StationPageButton_Clicked;
                buttonLayout.Add(StationPageButton);

                Button MainPageButton = new Button
                {
                    Text = "Select Aircraft",
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    Padding = 6,
                    Margin = 6,
                    WidthRequest = 150
                };
                MainPageButton.Clicked += MainPageButton_Clicked;
                buttonLayout.Add(MainPageButton);

                views.Add(buttonLayout, 0, 2);

                Content = new VerticalStackLayout
                {
                    BackgroundColor = Colors.White,
                    Children = {
                        views
                    }
                };
            }

            BindingContext = this;  
        }

        private void StationPageButton_Clicked(object? sender, EventArgs e)
        {
            Shell.Current.Navigation.PopAsync();
        }

        private void MainPageButton_Clicked(object? sender, EventArgs e)
        {
            Shell.Current.Navigation.PopToRootAsync();
        }
    }
}