using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Data;
using Octokit;

namespace GitHub.Converters
{
    public class LanguageFilesGitConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var languages = (value as Dictionary<string, GistFile>).Select(gf => gf.Value.Language).Distinct().ToList();

            string returnLanguages = string.Empty;

            for (int i = languages.Count - 1; i >= 0; i--)
            {
                returnLanguages += languages[i];

                if (i > 0)
                    returnLanguages += " / ";
            }

            return returnLanguages;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
