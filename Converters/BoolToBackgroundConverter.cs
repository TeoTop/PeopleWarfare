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
    public class BoolToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string root = System.AppDomain.CurrentDomain.BaseDirectory + "PeopleWarFare/WarfareWPF/";
            ImageBrush i1 = new ImageBrush(new BitmapImage(new Uri(root + "res/parcheminLargeTampon.png", UriKind.Relative)));
            ImageBrush i2 = new ImageBrush(new BitmapImage(new Uri(root + "res/parcheminLargeFin.png", UriKind.Relative)));
            return (bool)value ? i1 : i2;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
