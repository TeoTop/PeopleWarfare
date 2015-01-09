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
    public class RatioToPointsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            PointCollection points = new PointCollection();
            int zoom = (int)values[0];
            List<Point> default_point = (List<Point>)values[1];

            default_point.ForEach(p => points.Add(new Point(p.X * zoom / 100, p.Y * zoom / 100)));

            /*default_point.Split(' ').ToList().ForEach(d => {
                List<Double> p = d.Split(',').ToList().Select(s => System.Convert.ToDouble(s)).ToList();
                points.Add(new Point(p[0] * zoom / 100, p[1] * zoom / 100 ));
            });*/

            /*String points_string = points.Aggregate("", (acc, item) => acc + " " + item.X.ToString().Replace(",", ".") + "," + item.Y.ToString().Replace(",", "."));
            points_string = points_string.Substring(1, points_string.Length - 1);

            return points_string;*/

            return points;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
