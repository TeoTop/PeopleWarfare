using System;
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
    public class RatioToPointsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PointCollection defaut = new PointCollection();
            int zoom = (int)value;
            defaut.Add(new Point(0, 0));
            defaut.Add(new Point(40, 20));
            defaut.Add(new Point(80, 0));
            defaut.Add(new Point(80, -40));
            defaut.Add(new Point(40, -60));
            defaut.Add(new Point(0, -40));
            IEnumerable<Point> points = defaut.Select(item => new Point(item.X * zoom / 100, item.Y * zoom / 100));

            String points_string = points.Aggregate("", (acc, item) => acc + " " + item.X.ToString().Replace(",", ".") + "," + item.Y.ToString().Replace(",", "."));
            points_string = points_string.Substring(1, points_string.Length - 1);
            //Console.WriteLine(points_string);

            return points_string;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
            //return Binding.DoNothing;
        }
    }
}
