using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using GitHub.ViewModel.Abstract;
using Octokit;
using ReactiveUI;

namespace GitHub.ViewModel.Concrete
{
    public class UsersViewModel : ReactiveViewModelBase, IUsersViewModel
    {
        private readonly ObservableCollection<User> _users = new ObservableCollection<User>();
        public ObservableCollection<User> Users { get { return _users; } }

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

        public ReactiveCommand<SearchUsersResult> Search { get; private set; }


        public UsersViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.

                _users.Add(new User
                {
                    Login = "Odonno",
                    Followers = 144,
                    Following = 3,
                    PublicRepos = 44,
                    AvatarUrl = "https://github.com/identicons/odonno.png"
                });
                _users.Add(new User
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

            // Search part
            var canSearch = this.WhenAny(x => x.SearchValue, x => !string.IsNullOrWhiteSpace(x.Value));
            Search = ReactiveCommand.CreateAsyncTask(canSearch, async _ => await ViewModelLocator.GitHubService.SearchUsersAsync(SearchValue));
            

            Search.Subscribe(results =>
            {
                Users.Clear();
                foreach (var item in results.Items)
                    Users.Add(item);
            });

            Search.ThrownExceptions
                .Subscribe(ex => UserError.Throw("Potential Network Connectivity Error", ex));

            this.WhenAnyValue(x => x.SearchValue)
                .Throttle(TimeSpan.FromSeconds(1), RxApp.MainThreadScheduler)
                .InvokeCommand(this, x => x.Search);

            // TODO : first request on last registered users ?
        }
    }
}