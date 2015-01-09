﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Converters
{
    public class ConvertZoom : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)((int)value / 5);
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)((Double)value * 5);
        }
    }
}
