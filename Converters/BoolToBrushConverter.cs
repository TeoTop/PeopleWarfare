using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Converters
{
    public class BoolToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //string root = System.AppDomain.CurrentDomain.BaseDirectory + "PeopleWarFare/WarfareWPF/";
            ImageBrush i1 = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/res/parcheminFin.png", UriKind.Absolute)));
            ImageBrush i2 = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/res/parchemin.png", UriKind.Absolute)));
            return (bool)value ? i1 : i2;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
