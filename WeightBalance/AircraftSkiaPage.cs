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
        private SKBitmap chartBitmap;
        private Aircraft _aircraft;

        public AircraftSkiaPage(Aircraft aircraft)
        {
            _aircraft = aircraft;
            
            var respath = $"WeightBalance.Resources.Images.{_aircraft.AircraftImagePath}";
            canvasView = new SKCanvasView();
            canvasView.PaintSurface += OnCanvasView_PaintSurface;

            // Load aircraft image resource bitmap
            Assembly assembly = GetType().GetTypeInfo().Assembly;
            using (Stream stream = assembly.GetManifestResourceStream(respath))
            {
                resourceBitmap = SKBitmap.Decode(stream);
            }
            
            // Load aircraft image resource bitmap
            respath = $"WeightBalance.Resources.Images.{_aircraft.ChartImagePath}";
            using (Stream stream = assembly.GetManifestResourceStream(respath))
            {
                chartBitmap = SKBitmap.Decode(stream);
            }

            Button cogbutton = new Button { Text = "Edit Stations" };
            cogbutton.Padding = 6;
            cogbutton.Margin = 6;
            cogbutton.Clicked += Cogbutton_Clicked;
            
            Button homebutton = new Button { Text = "Aircraft List" };
            homebutton.Clicked += Homebutton_Clicked;
            homebutton.Padding = 6; 
            homebutton.Margin = 6;

            HorizontalStackLayout buttonbox = new HorizontalStackLayout();
            
            buttonbox.Children.Add(cogbutton);
            buttonbox.Children.Add(homebutton);

            Content = new Grid
            {
                canvasView,
                buttonbox
            };
            
            BindingContext = this;
        
        }

        private void Homebutton_Clicked(object? sender, EventArgs e)
        {
            Navigation.PopToRootAsync();
        }

        private void Cogbutton_Clicked(object? sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void OnCanvasView_PaintSurface(object? sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
            var top = info.Height / 5;

            if (resourceBitmap != null)
            {
                canvas.DrawBitmap(resourceBitmap, new SKRect(0, 0, info.Width, top));
                float chwd = (float)(info.Width / .7);
                //canvas.Scale(chwd);
                canvas.DrawBitmap(chartBitmap, new SKRect(60, top + 250, chwd - 60, info.Height - 80));
            }
        }
    }
}
