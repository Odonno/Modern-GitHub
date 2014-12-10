using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace GitHub.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool)
                return  (bool)value ? Visibility.Visible : Visibility.Collapsed;

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is Visibility)
                return ((Visibility)value == Visibility.Visible);

            return false;
        }
    }
}
