using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GitHub.ViewModels.ViewModel.Abstract;
using Octokit;

namespace GitHub.ViewModels.ViewModel.Concrete
{
    internal class UsersViewModel : ViewModelBase, IUsersViewModel
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