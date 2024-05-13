using System.Globalization;
using WeightBalance.Models;

namespace WeightBalance
{
    public class ColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            //var input = (value as CoGUnit).Station;
            return Colors.DarkCyan;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            //var input = (value as CoGUnit).Station;
            return Colors.LightCyan;
        }
    }
}
