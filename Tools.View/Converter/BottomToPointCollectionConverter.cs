using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using PeletonSoft.Tools.Model.SketchMath.Wave;

namespace PeletonSoft.Tools.View.Converter
{
 public 
     class BottomToPointCollectionConverter : IValueConverter
  {
    #region IValueConverter Members

    public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        var bottom = value as Bottom<IEnumerable<Point>>;
        return bottom == null ? null 
            : new PointCollection(bottom.Start.Concat(bottom.Finish.Reverse()));
    }

    public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      return null; //not needed
    }

    #endregion
  }
}
