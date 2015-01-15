using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using PeopleWar;


namespace Converters
{
    public class PeopleTypeToOpacityConverter : IMultiValueConverter
    {


        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            EnumPeuple select = (EnumPeuple)values[0];
            EnumPeuple peuple = (EnumPeuple)values[1];
            return select == peuple ? 1.0 : 0.0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
