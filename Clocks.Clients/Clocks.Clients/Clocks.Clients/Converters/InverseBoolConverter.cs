using System;
using System.Globalization;
using Xamarin.Forms;

namespace Clocks.Clients.Core.Converters
{
    /// <inheritdoc />
    /// <summary>
    /// Конвертер для инверсии bool
    /// </summary>
    public class InverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
            {
                throw new InvalidOperationException("Значение должно быть типа bool");
            }

            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
