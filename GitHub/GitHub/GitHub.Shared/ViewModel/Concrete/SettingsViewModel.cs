using System;
using System.Collections.Generic;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using GalaSoft.MvvmLight;
using GitHub.ViewModel.Abstract;

namespace GitHub.ViewModel.Concrete
{
    public class SettingsViewModel : ViewModelBase, ISettingsViewModel
    {
        #region Properties

        private readonly IEnumerable<string> _themes = new[] { "light", "dark" };
        public IEnumerable<string> Themes { get { return _themes; } }

        private string _selectedTheme;
        public string SelectedTheme
        {
            get { return _selectedTheme; }
            set { RequestSaveChanges(value); }
        }

        #endregion


        #region Constructor

        public SettingsViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
                _selectedTheme = "light";
            }
            else
            {
                // Code runs "for real"
                _selectedTheme = (string)(ApplicationData.Current.LocalSettings.Values["theme"] ?? "light");
            }
        }

        #endregion


        #region Methods

        private async void RequestSaveChanges(string value)
        {
            var messageDialog = new MessageDialog("You need to restart the app to apply changes", "Apply changes");

            messageDialog.Commands.Add(new UICommand("ok", command =>
            {
                ApplicationData.Current.LocalSettings.Values["theme"] = value;
                Application.Current.Exit();
            }));

            messageDialog.Commands.Add(new UICommand("cancel", command =>
            {
                _selectedTheme = (string)(ApplicationData.Current.LocalSettings.Values["theme"] ?? "light");
                RaisePropertyChanged("SelectedTheme");
            }));

            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 1;

            await messageDialog.ShowAsync();
        }

        #endregion
    }
}
