﻿using System.Globalization;
using TicTacTow25.Models;

namespace TicTacTow25.Converters
{
	internal class BoolToIconConverter : IValueConverter
	{

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			string icon = Icons.Visibility_off;
            if (value != null)
				icon =  (bool)value ? Icons.Visibility_off : Icons.Visibility_on;
			return icon;
		}

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
