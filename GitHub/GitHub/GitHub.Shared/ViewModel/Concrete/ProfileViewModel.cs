using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GitHub.Services.Abstract;
using GitHub.ViewModel.Abstract;
using Microsoft.Practices.ServiceLocation;
using Octokit;

namespace GitHub.ViewModel.Concrete
{
    public class ProfileViewModel : ViewModelBase, IProfileViewModel
    {
        public User CurrentUser { get; private set; }

        public ProfileViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.

                CurrentUser = new User
                {
                    Login = "Odonno",
                    AvatarUrl = "https://avatars3.githubusercontent.com/u/6053067",
                    Followers = 32,
                    Following = 4,
                    PublicRepos = 44,
                    PublicGists = 169,
                    Collaborators = 9,
                    TotalPrivateRepos = 0
                };
            }
            else
            {
                // Code runs "for real"

                Load();
            }
        }

        public async Task Load()
        {
#if DEBUG
            CurrentUser = await ServiceLocator.Current.GetInstance<IGitHubService>().GetUserAsync("Odonno");
#else
            CurrentUser = await ServiceLocator.Current.GetInstance<IGitHubService>().GetCurrentUserAsync();
#endif
        }
    }
}