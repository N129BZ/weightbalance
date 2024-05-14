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
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            using (Stream stream = assembly.GetManifestResourceStream(aircraft.ChartResourcePath))
            {
                image = PlatformImage.FromStream(stream);
            }
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            //canvas.DrawImage(image, 20, 60, 370, 360);

            /*******************************************************
             * Point plotter needs to know the image's rectangular
             * area, so make a rectangle with the same coordinates
             * to pass to the Plotter
             *******************************************************/ 
            Rect rect = new Rect(20, 20, 370, 360);
            PointF point = Plotter.PlotChartPoint(cog, rect, aircraft);

            IImage dot;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            using (Stream stream = assembly.GetManifestResourceStream(dotpath))
            {
                dot = PlatformImage.FromStream(stream);
            }
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            canvas.DrawImage(dot, point.X, point.Y, dot.Width, dot.Height);

        }
    }
}
