using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace GitHub.Converters
{
    public class ThemeToColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string theme = value.ToString();

            if (theme == "light" || theme == "dark")
            {
                if (parameter != null && parameter.ToString() == "inverse")
                    theme = (theme == "light") ? "dark" : "light";

                if (theme == "light")
                    return new SolidColorBrush(Colors.White);

                if (theme == "dark")
                    return new SolidColorBrush(Colors.Black);
            }

            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var color = (SolidColorBrush)value;
            
            if (color.Color == Colors.White || color.Color == Colors.Black)
            {
                if (parameter != null && parameter.ToString() == "inverse")
                    color.Color = (color.Color == Colors.White) ? Colors.Black : Colors.White;

                if (color.Color == Colors.White)
                    return "light";

                if (color.Color == Colors.Black)
                    return "dark";
            }

            throw new NotImplementedException();
        }
    }
}
