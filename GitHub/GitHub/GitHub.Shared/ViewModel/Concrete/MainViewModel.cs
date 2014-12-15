using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GitHub.ViewModel.Abstract;

namespace GitHub.ViewModel.Concrete
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        public IProfileViewModel ProfileViewModel { get; private set; }
        public IActivitiesViewModel ActivitiesViewModel { get; private set; }
        public IReposViewModel ReposViewModel { get; private set; }
        public IUsersViewModel UsersViewModel { get; private set; }


        private bool _canRefresh;
        public bool CanRefreshProperty
        {
            get { return _canRefresh; }
            private set
            {
                _canRefresh = value;
                ((RelayCommand)RefreshCommand).RaiseCanExecuteChanged();
            }
        }

        private bool _searchingEnabled;
        public bool SearchingEnabled
        {
            get { return _searchingEnabled; }
            private set
            {
                _searchingEnabled = value;
                RaisePropertyChanged();
            }
        }

        public ICommand ToggleEnableSearchCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand ShareIdeaCommand { get; private set; }
        public ICommand ContactSupportCommand { get; private set; }


        public MainViewModel()
        {
            ProfileViewModel = ViewModelLocator.Profile;
            ActivitiesViewModel = ViewModelLocator.Activities;
            ReposViewModel = ViewModelLocator.Repos;
            UsersViewModel = ViewModelLocator.Users;

            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                // Code runs "for real"

                ToggleEnableSearchCommand = new RelayCommand(ToggleEnableSearch);
                RefreshCommand = new RelayCommand(Refresh, CanRefresh);
                ShareIdeaCommand = new RelayCommand(GoToFeedbackWebsite);
                ContactSupportCommand = new RelayCommand(SendTicket);

                WaitForRefresh();
            }
        }

        private void ToggleEnableSearch()
        {
            SearchingEnabled = !SearchingEnabled;
        }

        private bool CanRefresh()
        {
            return CanRefreshProperty;
        }
        private async void Refresh()
        {
            CanRefreshProperty = false;

            // TODO : check for any bug !
            var refreshTasks = new List<Task>();

            var activitiesVm = ActivitiesViewModel as SearchViewModelBase;
            var reposVm = ReposViewModel as SearchViewModelBase;
            var usersVm = UsersViewModel as SearchViewModelBase;

            ProfileViewModel.Load();
            
            if (activitiesVm != null)
                refreshTasks.Add(activitiesVm.Refresh());
            if (reposVm != null)
                refreshTasks.Add(reposVm.Refresh());
            if (usersVm != null)
                refreshTasks.Add(usersVm.Refresh());

            foreach (var refreshTak in refreshTasks)
                await refreshTak;

            await WaitForRefresh();
        }


        private async Task WaitForRefresh()
        {
            await Task.Delay(60 * 1000);
            CanRefreshProperty = true;
        }


        private async void GoToFeedbackWebsite()
        {
            var feedbackUri = new Uri("http://dbottiau.uservoice.com/");
            await Launcher.LaunchUriAsync(feedbackUri);
        }


        private async void SendTicket()
        {
            var mailtoUri = new Uri("mailto:?to=tickets@dbottiau.uservoice.com&subject=[Modern GitHub] The subject&body=Describe your trouble about Modern GitHub here.");
            await Launcher.LaunchUriAsync(mailtoUri);
        }
    }
}