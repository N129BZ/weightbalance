using System.Reflection;
using IImage = Microsoft.Maui.Graphics.IImage;
using Microsoft.Maui.Graphics.Platform;
using WeightBalance.Models;

namespace WeightBalance.Drawables
{
    internal class MainChart : IDrawable
    {
        private Aircraft _aircraft;
        private float[] _position = new float[2];

        public MainChart(Aircraft aircraft) 
        { 
            _aircraft = aircraft;
            PositionCoG();
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            string imgpath = $"WeightBalance.Resources.Images.{_aircraft.ChartImagePath}";

            IImage image;
            Assembly assembly = GetType().GetTypeInfo().Assembly;
            using (Stream stream = assembly.GetManifestResourceStream(imgpath))
            {
                image = PlatformImage.FromStream(stream);
                canvas.DrawImage(image, 20, 120, 370, 360);
            }

            canvas.StrokeColor = Colors.Black;
            canvas.StrokeSize = 2;
            canvas.FillColor = Colors.Green;
            canvas.DrawCircle(-20, 180, 6);

            
        }

        private void PositionCoG()
        {
            var cog = _aircraft.CalculatedCoG;            
        }
    }
}
