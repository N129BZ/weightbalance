using System.Reflection;
using IImage = Microsoft.Maui.Graphics.IImage;
using Microsoft.Maui.Graphics.Platform;
using WeightBalance.Models;

namespace WeightBalance.Drawables
{
    internal class ChartOverlay : IDrawable
    {
        private Aircraft aircraft;
        private double[] position = new double[2];

        public ChartOverlay(Aircraft selectedaircraft) 
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

            IImage image;
            Assembly assembly = GetType().GetTypeInfo().Assembly;
            using (Stream stream = assembly.GetManifestResourceStream(aircraft.ChartResourcePath))
            {
                image = PlatformImage.FromStream(stream);
            }
            canvas.DrawImage(image, 20, 120, 370, 360);

            IImage dot;
            using (Stream stream = assembly.GetManifestResourceStream(dotpath))
            {
                dot = PlatformImage.FromStream(stream);
            }

            PointF point = Plotter.PlotChartPoint(cog, aircraft);

            canvas.DrawImage(dot, point.X, point.Y, dot.Width, dot.Height);

        }
    }
}
