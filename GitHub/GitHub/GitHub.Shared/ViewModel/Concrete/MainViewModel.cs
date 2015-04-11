using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using GitHub.ViewModel.Abstract;

namespace GitHub.ViewModel.Concrete
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        #region Services

        private readonly INavigationService _navigationService;

        #endregion


        #region Properties

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

        #endregion


        #region Commands

        public ICommand ToggleEnableSearchCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand GoToAboutCommand { get; private set; }

        #endregion


        #region Constructor

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

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
                GoToAboutCommand = new RelayCommand(GoToAbout);

                WaitForRefresh();
            }
        }

        #endregion


        #region Command methods

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

        private void GoToAbout()
        {
            _navigationService.NavigateTo("About");
        }

        #endregion


        #region Methods

        private async Task WaitForRefresh()
        {
            await Task.Delay(60 * 1000);
            CanRefreshProperty = true;
        }

        #endregion
    }
}