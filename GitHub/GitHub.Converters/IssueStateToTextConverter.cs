using System;
using Windows.UI.Xaml.Data;
using Octokit;

namespace GitHub.Converters
{
    public class IssueStateToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var issueState = (ItemState)value;

            if (issueState == ItemState.Open)
                return "\uf026";

            if (issueState == ItemState.Closed)
                return "\uf028";

            if (issueState == ItemState.All)
                return "\uf027";

            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
