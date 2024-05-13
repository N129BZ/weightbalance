using Microsoft.Maui.Graphics.Platform;
using IImage = Microsoft.Maui.Graphics.IImage;
using System.Reflection;
using WeightBalance.Models;

namespace WeightBalance.Drawables
{
    internal class AircraftOverlay : IDrawable
    {
        private Aircraft aircraft;

        public AircraftOverlay(Aircraft selectedaircraft)
        {
            aircraft = selectedaircraft;
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            string dotpath = aircraft.GreenDotResourcePath;
            
            var cog = aircraft.CoG;

            if ((aircraft.TotalWeight > aircraft.MaxGross) ||
                cog <= aircraft.MinCg || cog >= aircraft.MaxCg)
            {
                dotpath = aircraft.RedDotResourcePath;
            }

            IImage dot;
            Assembly assembly = GetType().GetTypeInfo().Assembly;
#pragma warning disable CS8600 
            using (Stream stream = assembly.GetManifestResourceStream(dotpath))
            {
                dot = PlatformImage.FromStream(stream);
            }
#pragma warning restore CS8600 

            canvas.FillColor = Color.FromRgba("#E8F8F570");
            canvas.StrokeColor = Colors.DarkGreen;
            canvas.StrokeSize = 1.4f;
            canvas.DrawRectangle(aircraft.CgRectangle);
            canvas.FillRectangle(aircraft.CgRectangle);

            PointF point = Plotter.PlotAircraftPoint(cog, aircraft);
            
            canvas.DrawImage(dot, point.X, point.Y, 15, 15);
        }
    }
}
