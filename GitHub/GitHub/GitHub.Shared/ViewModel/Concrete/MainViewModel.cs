using GalaSoft.MvvmLight;
using GitHub.ViewModel.Abstract;

namespace GitHub.ViewModel.Concrete
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        public IProfileViewModel ProfileViewModel { get; private set; }

        public IActivitiesViewModel ActivitiesViewModel { get; private set; }

        public IReposViewModel ReposViewModel { get; private set; }

        public IUsersViewModel UsersViewModel { get; private set; }


        public MainViewModel(IProfileViewModel profileViewModel, IActivitiesViewModel activitiesViewModel,
            IReposViewModel reposViewModel, IUsersViewModel usersViewModel)
        {
            //if (IsInDesignMode)
            //{
            //    // Code runs in Blend --> create design time data.
            //}
            //else
            //{
            //    // Code runs "for real"
            //}

            ProfileViewModel = profileViewModel;
            ActivitiesViewModel = activitiesViewModel;
            ReposViewModel = reposViewModel;
            UsersViewModel = usersViewModel;
        }
    }
}