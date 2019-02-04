using Clocks.Clients.Core.Models;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace Clocks.Clients.Core.Converters
{
    /// <inheritdoc />
    /// <summary>
    /// Конвертер типа элемента меню
    /// </summary>
    public class MenuItemTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var menuItemType = (MenuItemType)value;

            var platform = Device.RuntimePlatform == Device.UWP;

            switch (menuItemType)
            {
                case MenuItemType.Home:
                    return platform ? "Assets/ic_home.png" : "ic_home.png";
                case MenuItemType.Author:
                    return platform ? "Assets/ic_github.png" : "ic_github.png";
                case MenuItemType.Logout:
                    return platform ? "Assets/ic_logout.png" : "ic_logout.png";
                default:
                    return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
