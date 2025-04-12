using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Layout;
using SquarePixel.Models.AI;

namespace SquarePixel.Converters;

public class ChatHorizontalAlignmentConverter: IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not MessageSource source) throw new InvalidCastException();
        
        return source switch
        {
            MessageSource.User => HorizontalAlignment.Right,
            MessageSource.Model => HorizontalAlignment.Left,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}