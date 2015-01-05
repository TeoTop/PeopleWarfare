using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Converters
{
    public class RatioToMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int zoom = (int)value;
            String param = (String)parameter;
            String[] defaut = param.Split(new Char[] { ' ' });
            int defautLeft = System.Convert.ToInt32(defaut[0]);
            int defautTop = System.Convert.ToInt32(defaut[1]);
            int defautRight = System.Convert.ToInt32(defaut[2]);
            int defautBottom = System.Convert.ToInt32(defaut[3]);
            defautLeft = defautLeft * zoom / 100;
            defautTop = defautTop * zoom / 100;
            defautRight = defautRight * zoom / 100;
            defautBottom = defautBottom * zoom / 100;
            return defautLeft + " " + defautTop + " " + defautRight + " " + defautBottom;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
