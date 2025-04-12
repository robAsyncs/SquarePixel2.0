using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using SquarePixel.Models.AI;

namespace SquarePixel.Converters;

public class ChatColorConverter: IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not MessageSource source) throw new InvalidCastException();
        
        return source switch
        {
            MessageSource.User => new SolidColorBrush(new Color(255,193,140,93)),
            MessageSource.Model => new SolidColorBrush(new Color(255,234,196,53)),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}