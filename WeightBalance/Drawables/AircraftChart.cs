using WeightBalance.Models;

namespace WeightBalance.Drawables
{
    internal class AircraftChart
    {
        private Aircraft _aircraft;

        public AircraftChart(Aircraft aircraft)
        {
            _aircraft = aircraft;
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        { 
            canvas.StrokeColor = Colors.DarkGreen;
            canvas.FillColor = Colors.LightGreen;
            canvas.StrokeSize = 3;
            canvas.DrawRectangle(40, 30, 36, 160);

            canvas.StrokeColor = Colors.Black;
            canvas.StrokeSize = 2;
            canvas.FillColor = Colors.DarkGreen;
            canvas.DrawCircle(80, 180, 6);
            //canvas.FillCircle(80, 180, 6);
        }
    }
}
