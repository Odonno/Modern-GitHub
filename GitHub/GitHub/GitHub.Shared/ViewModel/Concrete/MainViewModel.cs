using System.Windows.Input;
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


        public MainViewModel()
        {
            ProfileViewModel = ViewModelLocator.Profile;
            ActivitiesViewModel = ViewModelLocator.Activities;
            ReposViewModel = ViewModelLocator.Repos;
            UsersViewModel = ViewModelLocator.Users;

            //if (IsInDesignMode)
            //{
            //    // Code runs in Blend --> create design time data.
            //}
            //else
            //{
            //    // Code runs "for real"
            //}

            ToggleEnableSearchCommand = new RelayCommand(ToggleEnableSearch);
        }

        private void ToggleEnableSearch()
        {
            SearchingEnabled = !SearchingEnabled;
        }
    }
}