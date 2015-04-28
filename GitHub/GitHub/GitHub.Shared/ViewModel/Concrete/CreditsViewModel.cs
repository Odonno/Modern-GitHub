using System;
using GalaSoft.MvvmLight;
using GitHub.Services.Abstract;
using GitHub.ViewModel.Abstract;
using Octokit;

namespace GitHub.ViewModel.Concrete
{
    public class CreditsViewModel : ViewModelBase, ICreditsViewModel
    {
        private readonly IProgressIndicatorService _progressIndicatorService;


        private User _odonno;
        public User Odonno
        {
            get { return _odonno; }
            private set { _odonno = value; RaisePropertyChanged(); }
        }

        private User _corentinMiq;
        public User CorentinMiq
        {
            get { return _corentinMiq; }
            private set { _corentinMiq = value; RaisePropertyChanged(); }
        }


        public CreditsViewModel(IProgressIndicatorService progressIndicatorService)
        {
            _progressIndicatorService = progressIndicatorService;

            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
                Odonno = new User("https://github.com/identicons/odonno.png",
                    null, null, 100, null, new DateTimeOffset(), 0, null, 144, 3, null, null, 0,
                    1, null,
                    "Odonno", "David Bottiau",
                    0, null, 0, 0, 44, null, false);

                CorentinMiq = new User("https://github.com/identicons/corentinMiq.png",
                    null, null, 100, null, new DateTimeOffset(), 0, null, 144, 3, null, null, 0,
                    1, null,
                    "CorentinMiq", "Corentin Miquée",
                    0, null, 0, 0, 44, null, false);
            }
            else
            {
                // Code runs "for real"
                LoadUsers();
            }
        }


        private async void LoadUsers()
        {
            await _progressIndicatorService.ShowAsync();

            Odonno = await ViewModelLocator.GitHubService.GetUserAsync("Odonno");
            CorentinMiq = await ViewModelLocator.GitHubService.GetUserAsync("CorentinMiq");

            await _progressIndicatorService.HideAsync();
        }
    }
}
