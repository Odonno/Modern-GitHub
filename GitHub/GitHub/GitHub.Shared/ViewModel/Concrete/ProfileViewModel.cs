using System.Windows.Input;
using GalaSoft.MvvmLight;
using GitHub.ViewModel.Abstract;
using Octokit;

namespace GitHub.ViewModel.Concrete
{
    public class ProfileViewModel : ViewModelBase, IProfileViewModel
    {
        public User CurrentUser { get; private set; }

        public ICommand LoadCommand { get; private set; }


        public ProfileViewModel()
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