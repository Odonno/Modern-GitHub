using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using GitHub.DataObjects.Concrete;
using GitHub.ViewModel.Abstract;
using Octokit;
using ReactiveUI;

namespace GitHub.ViewModel.Concrete
{
    public class UsersViewModel : SearchViewModelBase, IUsersViewModel
    {
        public UsersIncrementalLoadingCollection Users { get; set; }


        public UsersViewModel()
        {
            Users = SimpleIoc.Default.GetInstance<UsersIncrementalLoadingCollection>();

            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.

                Users.Add(new User
                {
                    Login = "Odonno",
                    Followers = 144,
                    Following = 3,
                    PublicRepos = 44,
                    AvatarUrl = "https://github.com/identicons/odonno.png"
                });
                Users.Add(new User
                {
                    Login = "CorentinMiq",
                    Followers = 7,
                    Following = 84,
                    PublicRepos = 3,
                    AvatarUrl = "https://github.com/identicons/CorentinMiq.png"
                });
            }
            else
            {
                // Code runs "for real"

                // TODO : first request on last registered users ?
            }
        }

        protected async override Task CompleteSearch()
        {
            Users.Reset(SearchValue);
            await Users.LoadMoreItemsAsync((uint)Users.ItemsPerPage);
        }
    }
}