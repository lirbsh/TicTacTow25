using System.Globalization;

namespace TicTacTow25.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            Color color = Colors.Cyan;
            if (value != null)
                if((bool)value )
                color = Colors.Yellow;
            return color;
        }
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
