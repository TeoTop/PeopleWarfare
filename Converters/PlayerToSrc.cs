using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using PeopleWar;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Converters
{
    public class PlayerToSrc : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int player = (int)values[0];
            String src = "../" + (String)values[player + 1];
            Uri imageUri = new Uri(src, UriKind.Relative);
            BitmapImage imageBitmap = new BitmapImage(imageUri);
            return (imageBitmap == null) ? DependencyProperty.UnsetValue : imageBitmap;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
