using System.Reflection;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;
using WeightBalance.Models;

namespace WeightBalance
{
    public class AircraftView : ContentView
    {
        private SKCanvasView canvasView;
        private SKBitmap bitmap;
        private Aircraft aircraft;

        public AircraftView(Aircraft currentaircraft)
        {
            aircraft = currentaircraft;
            
            var respath = $"WeightBalance.Resources.Images.{aircraft.AircraftImagePath}";
            canvasView = new SKCanvasView();
            canvasView.PaintSurface += OnCanvasView_PaintSurface;

            // Load aircraft image resource bitmap
            Assembly assembly = GetType().GetTypeInfo().Assembly;
            using (Stream stream = assembly.GetManifestResourceStream(respath))
            {
                bitmap = SKBitmap.Decode(stream);
            }

        }
            
        private void OnCanvasView_PaintSurface(object? sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
            var top = info.Height / 5;

            if (bitmap != null)
            {
                canvas.DrawBitmap(bitmap, new SKRect(0, 0, info.Width, top));
            }
        }
    }
}
