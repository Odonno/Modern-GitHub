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
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.

                CurrentUser = new User
                {
                    Login = "Odonno",
                    AvatarUrl = "https://avatars3.githubusercontent.com/u/6053067"
                };
            }
            else
            {
                // Code runs "for real"
            }
        }
    }
}