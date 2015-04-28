using System;
using System.Collections.Generic;
using System.Windows.Input;
using Windows.ApplicationModel.Resources;
using Windows.System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using GitHub.ViewModel.Abstract;

namespace GitHub.ViewModel.Concrete
{
    public class FeedbackViewModel : ViewModelBase, IFeedbackViewModel
    {
        private readonly ResourceLoader _resourceLoader = ResourceLoader.GetForCurrentView("Resources");
        private readonly INavigationService _navigationService;


        public IEnumerable<string> FeedbackTypes
        {
            get
            {
                return new List<string>
                {
                    _resourceLoader.GetString("suggestion"),
                    _resourceLoader.GetString("bug"),
                    _resourceLoader.GetString("other")
                };
            }
        }

        private string _selectedFeedbackType;
        public string SelectedFeedbackType
        {
            get { return _selectedFeedbackType; }
            set
            {
                _selectedFeedbackType = value;
                RaisePropertyChanged();
            }
        }

        private string _content;
        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
                RaisePropertyChanged();
                ((RelayCommand)SendFeedbackCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand SendFeedbackCommand { get; private set; }


        public FeedbackViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            SelectedFeedbackType = _resourceLoader.GetString("suggestion");

            SendFeedbackCommand = new RelayCommand(SendFeedback, CanSendFeedback);

            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
                Content = "I write a text for the feedback feature of the Modern GitHub app.";
            }
            else
            {
                // Code runs "for real"
            }
        }


        private bool CanSendFeedback()
        {
            return !string.IsNullOrWhiteSpace(Content);
        }
        private async void SendFeedback()
        {
            var stringUri = string.Format("mailto:?to={0}&subject={1}&body={2}",
                "david.bottiau@epsi.fr",
                string.Format("[Modern GitHub] {0}", SelectedFeedbackType),
                Content);

            await Launcher.LaunchUriAsync(new Uri(stringUri));

            App.TelemetryClient.TrackEvent(string.Format("SendFeedback.{0}", SelectedFeedbackType));

            _navigationService.GoBack();
            Content = string.Empty;
        }
    }
}
