using WeightBalance.Models;

namespace WeightBalance
{

    public partial class AircraftPage : ContentPage
    {
        internal class RectangleDrawable : IDrawable
        {
            private float[] _cgrect = [];
            public RectangleDrawable(float[] cgrect)
            {
                _cgrect = cgrect;
            }

            public void Draw(ICanvas canvas, RectF dirtyRect)
            {
                canvas.StrokeColor = Colors.DarkGreen;
                canvas.StrokeSize = 2;
                canvas.DrawRectangle(_cgrect[0], _cgrect[1], _cgrect[2], _cgrect[3]);
            }
        }

        public AircraftPage(Aircraft? aircraft)
        {
            if (aircraft == null)
            {
                throw new Exception("selectedAircraft cannot be null!");
            }
            else
            {
                var acd = new RectangleDrawable(aircraft.CgRectangle);
                var chartdot = new ChartDot();
                var aircraftdot = new AircraftDot();
                
                GraphicsView acrect = new GraphicsView
                {
                    Drawable = acd,
                    HeightRequest = 120,
                    WidthRequest = 120
                };

                GraphicsView chdot = new GraphicsView
                {
                    Drawable = chartdot,
                    HeightRequest = 200,
                    WidthRequest = 10
                };

                GraphicsView acdot = new GraphicsView
                {
                    Drawable = aircraftdot,
                    HeightRequest = 200,
                    WidthRequest = 10
                };

                Grid acgrid = new Grid
                {
                    aircraft.AcImage,
                    acrect,
                    acdot
                };

                Grid spacer = new Grid
                {
                    aircraft.Spacer
                };

                Grid chartgrid = new Grid
                {
                    aircraft.ChartImage,
                    chdot
                };

                Button backbtn = new Button
                {
                    Text = "View Stations",
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                };
                backbtn.Clicked += Button_Clicked;

                Grid btngrid = new Grid
                {   
                    HeightRequest = 100,
                    VerticalOptions = LayoutOptions.End,
                    HorizontalOptions = LayoutOptions.Center,
                };
                btngrid.Children.Add(backbtn);

                Content = new VerticalStackLayout
                {
                    Children = {
                        new Label
                        {
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Center,
                            FontAttributes = FontAttributes.Bold,
                            Text = $"{aircraft.Name.ToUpper()} MAX GROSS: {aircraft.MaxGross}"
                        },
                        acgrid,
                        spacer,
                        chartgrid,
                        btngrid
                    }
                };
            }
        }

        private void Button_Clicked(object? sender, EventArgs e)
        {
            Shell.Current.Navigation.PopAsync();
        }

        internal class ChartDot : IDrawable 
        {
            public void Draw(ICanvas canvas, RectF dirtyRect)
            {
                canvas.StrokeColor = Colors.Black;
                canvas.StrokeSize = 1;
                canvas.FillColor = Colors.DarkGreen;
                canvas.FillCircle(80, 180, 6);
            }
        }

        internal class AircraftDot : IDrawable //(float weight, float moment, Color bgcolor, Color fgcolor)
        {
            public void Draw(ICanvas canvas, RectF dirtyRect)
            {
                canvas.StrokeColor = Colors.Black;
                canvas.StrokeSize = 1;
                canvas.FillColor = Colors.Red;
                canvas.FillCircle(-20, 80, 6);
            }
        }
    }
}