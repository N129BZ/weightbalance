using WeightBalance.Models;
using WeightBalance.Drawables;

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
                Image acimage = new Image
                {
                    Source = acimgsrc,
                    Aspect = Aspect.AspectFit
                };
                GraphicsView acoverlay = new GraphicsView
                {
                    Drawable = new AircraftOverlay(aircraft),
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
                
                HorizontalStackLayout buttonLayout = new HorizontalStackLayout { HorizontalOptions = LayoutOptions.Center };
                buttonLayout.SetValue(Grid.RowProperty, 2);
                Button StationPageButton = new Button
                {
                    Text = "View Stations",
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Start,
                    Padding = 3,
                    Margin = 3,
                    WidthRequest = 130,
                    HeightRequest = 40,
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
                    Padding = 3,
                    Margin = 3,
                    WidthRequest = 130,
                    HeightRequest = 40,
                    BackgroundColor = Colors.Navy,
                    TextColor = Colors.White,
                    FontAttributes = FontAttributes.Bold
                };
                MainPageButton.Clicked += MainPageButton_Clicked;
                buttonLayout.Add(MainPageButton);

                Button ExitApp = new Button
                {
                    Text = "Taxi Out",
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.End,
                    Padding = 3,
                    Margin = 3,
                    WidthRequest = 130,
                    HeightRequest = 40,
                    BackgroundColor = Colors.Navy,
                    FontAttributes = FontAttributes.Bold,
                };
                ExitApp.Clicked += ExitApp_Clicked;
                buttonLayout.Add(ExitApp);  

                view.Add(acimage, 0, 0);
                view.Add(acoverlay, 0, 0);
                view.Add(chimage, 0, 1);
                view.Add(chartoverlay, 0, 1);
                view.Add(buttonLayout, 0, 2);

                Content = view;
            }

            BindingContext = this;  
        }

        private void ExitApp_Clicked(object? sender, EventArgs e)
        {
            Application.Current?.Quit();
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