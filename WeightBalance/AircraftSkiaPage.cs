using System.Reflection;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;
using WeightBalance.Models;

namespace WeightBalance
{
    public class AircraftSkiaPage : ContentPage
    {
        private SKCanvasView canvasView;
        private SKBitmap resourceBitmap;
        private Aircraft _aircraft;

        public AircraftSkiaPage(Aircraft aircraft)
        {
            _aircraft = aircraft;

            var respath = $"WeightBalance.Resources.Images.{_aircraft.AircraftImagePath}";
            canvasView = new SKCanvasView();
            canvasView.PaintSurface += OnCanvasView_PaintSurface;

            // Load resource bitmap
            Assembly assembly = GetType().GetTypeInfo().Assembly;

            

        }

        private void OnCanvasView_PaintSurface(object? sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            if (resourceBitmap != null)
            {
                canvas.DrawBitmap(resourceBitmap,
                    new SKRect(0, info.Height / 3, info.Width, 2 * info.Height / 3));
            }
        }
    }
}
