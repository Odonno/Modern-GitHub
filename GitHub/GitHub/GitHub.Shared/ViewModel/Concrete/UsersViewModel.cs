using System;
using System.Reactive;
using System.Reactive.Linq;
using GalaSoft.MvvmLight.Ioc;
using GitHub.DataObjects.Concrete;
using GitHub.ViewModel.Abstract;
using Octokit;
using ReactiveUI;

namespace GitHub.ViewModel.Concrete
{
    public class UsersViewModel : ReactiveViewModelBase, IUsersViewModel
    {
        public UsersIncrementalLoadingCollection Users { get; set; }

        private string _searchValue;
        public string SearchValue
        {
            get { return _searchValue; }
            set
            {
                _searchValue = value;
                this.RaisePropertyChanged();
            }
        }

        public ReactiveCommand<Unit> Search { get; private set; }


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
            }


            // TODO : first request on last registered users ?

            // Search part
            var canSearch = this.WhenAny(x => x.SearchValue, x => !string.IsNullOrWhiteSpace(x.Value));
            Search = ReactiveCommand.CreateAsyncTask(canSearch, async _ =>
            {
                Users.Reset(SearchValue);
                await Users.LoadMoreItemsAsync((uint)Users.ItemsPerPage);
            });

            Search.ThrownExceptions
                .Subscribe(ex => UserError.Throw("Potential Network Connectivity Error", ex));

            this.WhenAnyValue(x => x.SearchValue)
                .Throttle(TimeSpan.FromSeconds(1), RxApp.MainThreadScheduler)
                .InvokeCommand(this, x => x.Search);
        }
    }
}