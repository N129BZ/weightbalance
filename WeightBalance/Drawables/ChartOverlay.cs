using Microsoft.Maui.Graphics.Platform;
using System.Reflection;
using WeightBalance.Models;
using IImage = Microsoft.Maui.Graphics.IImage;

namespace WeightBalance.Drawables
{
    internal class ChartOverlay : IDrawable
    {
        private Aircraft _aircraft = new();
        public Aircraft SelectedAircraft 
        { 
            get { return _aircraft; } 
            set { _aircraft = value; }
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            string dotpath = Aircraft.GreenDotResourcePath;

            var cog = _aircraft.CoG;

            if ((_aircraft.TotalWeight > _aircraft.MaxGross) ||
                cog <= _aircraft.MinCg || cog >= _aircraft.MaxCg)
            {
                dotpath = Aircraft.RedDotResourcePath;
            }

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            IImage image;
            Assembly assembly = GetType().GetTypeInfo().Assembly;
            using (Stream stream = assembly.GetManifestResourceStream(_aircraft.ChartResourcePath))
            {
                image = PlatformImage.FromStream(stream);
            }

            /*******************************************************
             * Point plotter needs to know the image's rectangular
             * area, so make a rectangle with the same coordinates
             * to pass to the Plotter, height is minus dot radius
             *******************************************************/
            Rect rect = new(20, -5, 370, 360);
            PointF point = Plotter.PlotChartPoint(cog, rect, _aircraft);

            IImage dot;
            using (Stream stream = assembly.GetManifestResourceStream(dotpath))
            {
                dot = PlatformImage.FromStream(stream);
            }
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            canvas.DrawImage(dot, point.X, point.Y, dot.Width, dot.Height);

        }
    }
}
