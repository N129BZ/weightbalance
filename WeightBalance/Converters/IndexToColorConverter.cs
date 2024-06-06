using System.Globalization;
using Syncfusion.Maui.ListView;

namespace WeightBalance.Converters;


public class IndexToColorConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var listview = parameter as SfListView;
        var index = listview.DataSource.DisplayItems.IndexOf(value);
        Color CornBlue = Colors.CornflowerBlue;
        Color Blue = Colors.LightBlue;

        if (index % 2 == 1)
        {
            return index % 2 == 0 ? CornBlue : Blue;
        }
        else
        {
            int row = index;
            if (row % 2 == 0)
            {
                return index % 2 == 0 ? CornBlue : Blue;
            }
            else
            {
                return index % 2 == 0 ? Blue : CornBlue;
            }
        }
    }

    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}