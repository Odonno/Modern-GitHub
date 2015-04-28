using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Octokit;

namespace GitHub.Converters
{
    public class IssueStateToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var issueState = (ItemState)value;

            if (issueState == ItemState.Open)
                return new SolidColorBrush(Colors.GreenYellow);

            if (issueState == ItemState.Closed)
                return new SolidColorBrush(Colors.DarkRed);

            if (issueState == ItemState.All)
                return new SolidColorBrush(Colors.GreenYellow);

            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
