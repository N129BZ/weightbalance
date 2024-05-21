using Microsoft.Maui.Graphics.Platform;
using System.Reflection;
using WeightBalance.Models;
using IImage = Microsoft.Maui.Graphics.IImage;

namespace WeightBalance.Drawables;

internal class AircraftOverlay : IDrawable
{
    private Aircraft aircraft = new();

    private double CoG;

    public AircraftOverlay(Aircraft selectedAircraft, double cog)
    {
        aircraft = selectedAircraft;
        CoG = cog;
    }

    //public Aircraft SelectedAircraft
    //{
    //    get { return aircraft; }
    //    set 
    //    { 
    //        aircraft = value;
    //        CoG = aircraft.CoG;
    //    }
    //}

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        string dotpath = Aircraft.GreenDotResourcePath;

        if ((aircraft.TotalWeight > aircraft.MaxGross) ||
            CoG <= aircraft.MinCg || CoG >= aircraft.MaxCg)
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
        canvas.DrawRectangle(aircraft.CgRectangle);
        canvas.FillRectangle(aircraft.CgRectangle);

        PointF point = Plotter.PlotAircraftPoint(CoG, aircraft);

        canvas.DrawImage(dot, point.X, point.Y, 15, 15);
    }
}
