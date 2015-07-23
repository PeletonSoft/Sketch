using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using PeletonSoft.Tools.View.Drawing;

namespace PeletonSoft.Tools.View.Converter
{
    public class PointCollectionToPathGeometryConverter : IValueConverter
    {
        public bool IsClosed { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<Point>)
            {
                return (value as IEnumerable<Point>).ToPathGeometry(IsClosed); 
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public PointCollectionToPathGeometryConverter()
        {
            IsClosed = true;
        }
    }
}