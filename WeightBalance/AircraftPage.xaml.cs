using WeightBalance.Models;
using WeightBalance.Drawables;

namespace WeightBalance
{
    public partial class AircraftPage : ContentPage
    {
        private string actitle = String.Empty;
        public string AcTitle { get { return actitle; } }

        public AircraftPage(Aircraft aircraft)
        {
            var cg = Math.Round(aircraft.CoG, 2);
            actitle = $"GROSS WT: {aircraft.TotalWeight}, CG: {cg}";
            
            InitializeComponent();

            if (aircraft == null)
            {
                throw new Exception("selectedAircraft cannot be null!");
            }
            else
            {
                this.Title = AcTitle;

                Grid views = new Grid();
                views.RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition(new GridLength(220)),
                    new RowDefinition(new GridLength(480)),
                    new RowDefinition(new GridLength(100))
                };
                
                ImageSource acimgsrc = ImageSource.FromResource(aircraft.AircraftResourcePath);
                Image acimage = new Image
                {
                    Source = acimgsrc,
                    Aspect = Aspect.AspectFit
                };
                GraphicsView acoverlay = new GraphicsView
                {
                    Drawable = new AircraftOverlay(aircraft)
                };
                
                ImageSource chimgsrc = ImageSource.FromResource(aircraft.ChartResourcePath);
                Image chimage = new Image
                {
                    Source = chimgsrc,
                    Aspect = Aspect.AspectFit
                };
                GraphicsView chartoverlay = new GraphicsView
                {
                    Drawable = new ChartOverlay(aircraft)
                };

                views.Add(acimage);
                views.Add(acoverlay);
                views.Add(chartoverlay, 0, 1);
                
                HorizontalStackLayout buttonLayout = new HorizontalStackLayout { HorizontalOptions = LayoutOptions.Center };
                Button StationPageButton = new Button
                {
                    Text = "View Stations",
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    Padding = 6,
                    Margin = 6,
                    WidthRequest = 150,
                    BackgroundColor = Colors.Navy,
                    TextColor = Colors.White,
                    FontAttributes = FontAttributes.Bold
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
                    WidthRequest = 150,
                    BackgroundColor = Colors.Navy,
                    TextColor = Colors.White,
                    FontAttributes = FontAttributes.Bold
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