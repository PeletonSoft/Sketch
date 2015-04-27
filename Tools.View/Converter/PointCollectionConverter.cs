using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace PeletonSoft.Tools.View.Converter
{
 public 
     class PointCollectionConverter : IValueConverter
  {
    #region IValueConverter Members

    public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        var points = value as IEnumerable<Point>;
        return points == null ? null : new PointCollection(points);
    }

    public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      return null; //not needed
    }

    #endregion
  }
}
