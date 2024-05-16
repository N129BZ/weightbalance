namespace WeightBalance.Models
{
    public static class Plotter
    {
        private static double PlotY(Rect chart, double maxgross, double mingross, double weight)
        {
            var range = maxgross - mingross;
            var pxfactor = chart.Height / range;
            var diff = maxgross - weight;
            var offset = chart.Top + diff * pxfactor;
            return offset - 5; // minus 5px dot radius
        }

        private static double PlotX(Rect chart, double maxcg, double mincg, double cog)
        {
            var range = maxcg - mincg;
            var pxfactor = chart.Width / range;
            var diff = maxcg - cog;
            var offset = chart.X + chart.Width - diff * pxfactor;
            return offset - 5; // minus 5px dot radius
        }

        public static Point PlotAircraftPoint(double cog, Aircraft aircraft)
        {
            var rect = aircraft.CgRectangle;
            var maxcg = aircraft.MaxCg;
            var mincg = aircraft.MinCg;

            var X = PlotX(rect, maxcg, mincg, cog);
            var Y = PlotY(rect, aircraft.MaxGross, aircraft.MinGross, aircraft.TotalWeight);
            return new Point(X, Y);

        }

        public static Point PlotChartPoint(double cog, Rect chartrect, Aircraft aircraft)
        {
            /************************************************* 
             * chart X = 20, Y = 120, W = 370, H = 360
             * chart left is MinCg, chart bottom is MinGross
             * So - our point needs to be offset from the 
             * left and bottom sides of the chart...
             ************************************************/

            // X = 20;        // this is the left of the chart, = MinCg 
            // Y = 120 + 360; // this is the bottom of the chart, = MinGross
            // W = 20 + 370;  // this is the right of chart, = MaxCg

            var maxcg = aircraft.MaxCg;
            var mincg = aircraft.MinCg;

            //Rect rect = new Rect(20, 60, 370, 360);

            double dotX = PlotX(chartrect, maxcg, mincg, cog);
            double dotY = PlotY(chartrect, aircraft.MaxGross, aircraft.MinGross, aircraft.TotalWeight);

            PointF point = new Point(dotX, dotY);
            return point;
        }
    }


}
