using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Converters
{
    public class PositionToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            Point InitialPosition = (Point)value[0];
            int Row = (int)value[1];
            int Column = (int)value[2];
            return (Row == InitialPosition.X && Column == InitialPosition.Y) ? Visibility.Visible : Visibility.Collapsed;
        }
        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
