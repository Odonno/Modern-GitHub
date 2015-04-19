using System;
using Windows.UI.Xaml.Data;

namespace GitHub.Converters
{
    public class ShortingTextConverter : IValueConverter
    {
        private const int MaxLength = 60;


        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string longText = value.ToString();

            if (longText.Length > MaxLength || longText.Contains("\n"))
            {
                var splitString = longText.Split(new[] { "\n" }, StringSplitOptions.None);

                if (splitString.Length > 0)
                {
                    string shortText = splitString[0];
                    return (shortText.Length > MaxLength) ? 
                        string.Format("{0}...", shortText.Substring(0, MaxLength)) : 
                        shortText;
                }
            }

            return longText;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
