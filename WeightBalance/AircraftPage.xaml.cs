using WeightBalance.Views;

namespace WeightBalance;


public partial class AircraftPage : ContentPage
{

    internal class RectangleDrawable : IDrawable
    {
        private float[]? _cgrect;
        public RectangleDrawable(float[] cgrect)
        {
            _cgrect = cgrect;
        }

        private ICanvas? _canvas;

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            _canvas = canvas;
            canvas.StrokeColor = Colors.DarkGreen;
            canvas.StrokeSize = 2;
            canvas.DrawRectangle(_cgrect[0], _cgrect[1], _cgrect[2], _cgrect[3]);
        }
    }

    public AircraftPage(Aircraft selectedAircraft)
    {
        var acd = new RectangleDrawable(selectedAircraft.CgRectangle);
        var chartdot = new ChartDot();
        var aircraftdot = new AircraftDot();
        var cogs = new Questions(selectedAircraft);

        GraphicsView acrect = new GraphicsView
        {
            Drawable = acd,
            HeightRequest = 120,
            WidthRequest = 120
        };

        GraphicsView chdot = new GraphicsView
        {
            Drawable = chartdot,
            HeightRequest = 120,
            WidthRequest = 120
        };

        GraphicsView acdot = new GraphicsView
        {
            Drawable = aircraftdot,
            HeightRequest = 120,
            WidthRequest = 120
        };

        Grid acgrid = new Grid 
        { 
            selectedAircraft.AcImage,
            acrect,
            acdot
        };

        Grid chartgrid = new Grid
        {
            selectedAircraft.ChartImage,
            chdot
        };

        Content = new VerticalStackLayout
        {
            Children = {
                new Label
                {
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    Text = selectedAircraft.Name.ToUpper()
                },
                acgrid,
                chartgrid,
                cogs
            }
        };
    }

    private void Button_Clicked(object? sender, EventArgs e)
    {
        Shell.Current.Navigation.PopToRootAsync();
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