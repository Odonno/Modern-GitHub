using System.Collections.Generic;

namespace GitHub.ViewModel.Abstract
{
    public interface ISettingsViewModel
    {
        IEnumerable<string> Themes { get; }
        string SelectedTheme { get; set; }
    }
}
