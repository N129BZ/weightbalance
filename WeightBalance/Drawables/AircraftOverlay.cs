using Microsoft.Maui.Graphics.Platform;
using System.Reflection;
using WeightBalance.Models;
using IImage = Microsoft.Maui.Graphics.IImage;

namespace WeightBalance.Drawables;

internal class AircraftOverlay(Aircraft selectedAircraft, double cog) : IDrawable
{
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        string dotpath = Aircraft.GreenDotResourcePath;

        if ((selectedAircraft.TotalWeight > selectedAircraft.MaxGross) ||
            cog <= selectedAircraft.MinCg || cog >= selectedAircraft.MaxCg)
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
        canvas.DrawRectangle(selectedAircraft.CgRectangle);
        canvas.FillRectangle(selectedAircraft.CgRectangle);

        PointF point = Plotter.PlotAircraftPoint(cog, selectedAircraft);

        canvas.DrawImage(dot, point.X, point.Y, 15, 15);
    }
}
