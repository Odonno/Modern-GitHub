using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using GitHub.ViewModel.Abstract;
using Octokit;

namespace GitHub.ViewModel.Concrete
{
    public class ProfileViewModel : ViewModelBase, IProfileViewModel
    {
        private readonly INavigationService _navigationService;

        public User CurrentUser { get; private set; }

        public ICommand GoToFollowersCommand { get; private set; }
        public ICommand GoToFollowingsCommand { get; private set; }
        public ICommand GoToCollaboratorsCommand { get; private set; }
        public ICommand GoToPublicReposCommand { get; private set; }
        public ICommand GoToPublicGistsCommand { get; private set; }
        public ICommand GoToPrivateReposCommand { get; private set; }


        public ProfileViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.

                CurrentUser = new User("https://github.com/identicons/odonno.png",
                    null, null, 100, null, new DateTimeOffset(), 0, null, 144, 3, null, null, 0,
                    1, null,
                    "Odonno", "David Bottiau",
                    0, null, 0, 0, 44, null, false);
            }
            else
            {
                // Code runs "for real"
                
                GoToFollowersCommand = new RelayCommand(GoToFollowers);
                GoToFollowingsCommand = new RelayCommand(GoToFollowings);
                GoToCollaboratorsCommand = new RelayCommand(GoToCollaborators);
                GoToPublicReposCommand = new RelayCommand(GoToPublicRepos);
                GoToPublicGistsCommand = new RelayCommand(GoToPublicGists);
                GoToPrivateReposCommand = new RelayCommand(GoToPrivateRepos);
            }
        }

        public async Task LoadAsync()
        {
#if DEBUG
            CurrentUser = await ViewModelLocator.GitHubService.GetUserAsync("Odonno");
#else
            CurrentUser = await ViewModelLocator.GitHubService.GetCurrentUserAsync();
#endif
            RaisePropertyChanged("CurrentUser");
        }

        private void GoToFollowers()
        {
            // TODO : implement new page
            _navigationService.NavigateTo("InDevelopment");
        }

        private void GoToFollowings()
        {
            // TODO : implement new page
            _navigationService.NavigateTo("InDevelopment");
        }

        private void GoToCollaborators()
        {
            // TODO : implement new page
            _navigationService.NavigateTo("InDevelopment");
        }

        private void GoToPublicRepos()
        {
            // TODO : implement new page
            _navigationService.NavigateTo("InDevelopment");
        }

        private void GoToPublicGists()
        {
            // TODO : implement new page
            _navigationService.NavigateTo("InDevelopment");
        }

        private void GoToPrivateRepos()
        {
            // TODO : implement new page
            _navigationService.NavigateTo("InDevelopment");
        }
    }
}