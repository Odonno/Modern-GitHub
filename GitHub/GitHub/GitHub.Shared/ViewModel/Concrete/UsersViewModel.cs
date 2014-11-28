using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GitHub.ViewModel.Abstract;
using Octokit;

namespace GitHub.ViewModel.Concrete
{
    public class UsersViewModel : ViewModelBase, IUsersViewModel
    {
        public ObservableCollection<User> Users { get; private set; }
        public ICommand SearchCommand { get; private set; }

        public UsersViewModel()
        {
            //if (IsInDesignMode)
            //{
            //    // Code runs in Blend --> create design time data.
            //}
            //else
            //{
            //    // Code runs "for real"
            //}
        }
    }
}