using Microsoft.Maui.Graphics.Platform;
using System.Reflection;
using WeightBalance.Models;
using Font = Microsoft.Maui.Graphics.Font;
using IImage = Microsoft.Maui.Graphics.IImage;

namespace WeightBalance.Drawables;

internal class ChartOverlay(Aircraft aircraft, double cog) : IDrawable
{
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        string dotpath = Aircraft.GreenDotResourcePath;

        if ((aircraft.TotalWeight > aircraft.MaxGross) ||
            cog <= aircraft.MinCg || cog >= aircraft.MaxCg)
        {
            dotpath = Aircraft.RedDotResourcePath;
        }

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        IImage image;
        Assembly assembly = GetType().GetTypeInfo().Assembly;
        using (Stream stream = assembly.GetManifestResourceStream(aircraft.ChartResourcePath))
        {
            image = PlatformImage.FromStream(stream);
        }

        /*******************************************************
         * Point plotter needs to know the image's rectangular
         * area, so make a rectangle with the same coordinates
         * to pass to the Plotter, height is minus dot radius
         *******************************************************/
        Rect rect = new(20, -5, 370, 360);
        PointF point = Plotter.PlotChartPoint(cog, rect, aircraft);

        IImage dot;
        using (Stream stream = assembly.GetManifestResourceStream(dotpath))
        {
            dot = PlatformImage.FromStream(stream);
        }
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

        canvas.DrawImage(dot, point.X, point.Y, dot.Width, dot.Height);

        canvas.StrokeColor = Colors.Black;
        canvas.StrokeSize = 2;
        canvas.FontSize = 12;
        canvas.Font = Font.Default;
        var acg = aircraft.CoG.ToString("#0.00");
        canvas.DrawString($"cg:{acg}", point.X + 8, point.Y + 34, HorizontalAlignment.Center);

        var mncg = aircraft.MinCg.ToString("#0.00");
        var mxcg = aircraft.MaxCg.ToString("#0.00");
        canvas.DrawString($"Range: {mncg} - {mxcg}", 200, 321, HorizontalAlignment.Justified);

        if (!aircraft.IsWithinWeight)
        {
            canvas.FontSize = 14;
            canvas.Font = Font.DefaultBold;
            canvas.FontColor = Colors.Red;
            canvas.DrawString("* OVER MAX GROSS *", 200, 190, HorizontalAlignment.Justified);
        }

        var minwt = aircraft.MinGross.ToString("#0.0");
        var maxwt = aircraft.MaxGross.ToString("#0.0");
        canvas.StrokeSize = 2;
        canvas.FontSize = 12;
        canvas.Font = Font.Default;
        canvas.Rotate(-90, point.X, point.Y + 352); 
        canvas.DrawString($"Range: {minwt} - {maxwt}", 354, 306, HorizontalAlignment.Justified);
    }
}
