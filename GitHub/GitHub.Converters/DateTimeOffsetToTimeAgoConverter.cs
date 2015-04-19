using System;
using Windows.UI.Xaml.Data;

namespace GitHub.Converters
{
    public class DateTimeOffsetToTimeAgoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var dateTimeOffset = (DateTimeOffset)value;
            var timeSpanDiff = new DateTimeOffset(DateTime.Now).Subtract(dateTimeOffset);

            if (timeSpanDiff.TotalSeconds < 60)
                return string.Format("{0} second{1} ago", timeSpanDiff.Seconds, timeSpanDiff.Seconds > 1 ? "s" : "");

            if (timeSpanDiff.TotalMinutes < 60)
                return string.Format("{0} minute{1} ago", timeSpanDiff.Minutes, timeSpanDiff.Minutes > 1 ? "s" : "");

            if (timeSpanDiff.TotalHours < 24)
                return string.Format("{0} hour{1} ago", timeSpanDiff.Hours, timeSpanDiff.Hours > 1 ? "s" : "");
            
            return string.Format("{0} day{1} ago", timeSpanDiff.Days, timeSpanDiff.Days > 1 ? "s" : "");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
