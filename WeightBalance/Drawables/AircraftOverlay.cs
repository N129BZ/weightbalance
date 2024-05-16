using Microsoft.Maui.Graphics.Platform;
using System.Reflection;
using WeightBalance.Models;
using IImage = Microsoft.Maui.Graphics.IImage;

namespace WeightBalance.Drawables
{
    internal class AircraftOverlay : IDrawable
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

#pragma warning disable CS8600 
            IImage dot;
            Assembly assembly = GetType().GetTypeInfo().Assembly;
            using (Stream stream = assembly.GetManifestResourceStream(dotpath))
            {
                dot = PlatformImage.FromStream(stream);
            }
#pragma warning restore CS8600 

            canvas.FillColor = Color.FromRgba("#E8F8F570");
            canvas.StrokeColor = Colors.DarkGreen;
            canvas.StrokeSize = 1.4f;
            canvas.DrawRectangle(_aircraft.CgRectangle);
            canvas.FillRectangle(_aircraft.CgRectangle);

            PointF point = Plotter.PlotAircraftPoint(cog, _aircraft);

            canvas.DrawImage(dot, point.X, point.Y, 15, 15);
        }
    }
}
