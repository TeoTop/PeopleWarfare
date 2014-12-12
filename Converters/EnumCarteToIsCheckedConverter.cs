using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using PeopleWar;

namespace Converters
{
    public class EnumCarteToIsCheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            EnumCarte val = (EnumCarte)value;
            EnumCarte param = (EnumCarte)Enum.Parse(typeof(EnumCarte), (String)parameter);
            return val == param;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }
}
