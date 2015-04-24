using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GitHub.Common;
using GitHub.Services.Abstract;
using GitHub.ViewModel.Abstract;
using Octokit;

namespace GitHub.ViewModel.Concrete
{
    public class UserViewModel : ViewModelBase, IUserViewModel
    {
        private readonly IProgressIndicatorService _progressIndicatorService;


        private User _user;
        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                RaisePropertyChanged();
                Reset();
                CanFollow = User.Login != ViewModelLocator.Profile.CurrentUser.Login &&
                            User.Type == AccountType.User;
            }
        }

        private bool _isFollowing;
        public bool IsFollowing
        {
            get { return _isFollowing; }
            private set
            {
                _isFollowing = value;
                RaisePropertyChanged();
            }
        }

        private bool _canFollow;
        public bool CanFollow
        {
            get { return _canFollow; }
            private set
            {
                _canFollow = value;
                RaisePropertyChanged();
                ((RelayCommand)FollowCommand).RaiseCanExecuteChanged();
            }
        }

        private readonly ObservableCollection<Activity> _activities = new ObservableCollection<Activity>();
        public ObservableCollection<Activity> Activities { get { return _activities; } }

        private readonly ObservableCollection<Repository> _repositories = new ObservableCollection<Repository>();
        public ObservableCollection<Repository> Repositories { get { return _repositories; } }


        public ICommand FollowCommand { get; private set; }
        public ICommand UnfollowCommand { get; private set; }


        public UserViewModel(IProgressIndicatorService progressIndicatorService)
        {
            _progressIndicatorService = progressIndicatorService;

            FollowCommand = new RelayCommand(Follow, () => CanFollow);
            UnfollowCommand = new RelayCommand(Unfollow);

            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data

                User = new User("https://github.com/identicons/odonno.png",
                    null, null, 100, null, new DateTimeOffset(), 0, null, 144, 3, null, null, 0,
                    1, null,
                    "Odonno", "David Bottiau",
                    0, null, 0, 0, 44, null, false);

                var firstRepo = new Repository(null, null, null, null, null, null, null,
                    1,
                    User,
                    "First-Repository",
                    "Odonno/First-Repository",
                    null, null, null,
                    false, false,
                    0, 0, 0,
                    "master",
                    0,
                    null, new DateTimeOffset(), new DateTimeOffset(),
                    null, null, null, null,
                    false, false, false);

                var anotherRepo = new Repository(null, null, null, null, null, null, null,
                    2,
                    User,
                    "Another-Repository",
                    "Odonno/Another-Repository",
                    null, null, null,
                    false, false,
                    0, 0, 0,
                    "master",
                    0,
                    null, new DateTimeOffset(), new DateTimeOffset(),
                    null, null, null, null,
                    false, false, false);

                Repositories.Add(firstRepo);
                Repositories.Add(anotherRepo);

                var pushActivity = new Activity("PushEvent", true, firstRepo, User, null,
                    new DateTimeOffset(new DateTime(2014, 12, 12)), "1");

                var createActivity = new Activity("CreateEvent", true, firstRepo, User, null,
                    new DateTimeOffset(new DateTime(2014, 12, 11)), "2");

                Activities.Add(pushActivity);
                Activities.Add(createActivity);
            }
            else
            {
                // Code runs "for real"
            }
        }


        private async void Follow()
        {
            await _progressIndicatorService.ShowAsync();

            if (await ViewModelLocator.GitHubService.FollowUserAsync(User.Login))
                IsFollowing = true;

            await _progressIndicatorService.HideAsync();
        }

        private async void Unfollow()
        {
            await _progressIndicatorService.ShowAsync();

            await ViewModelLocator.GitHubService.UnfollowUserAsync(User.Login);
            IsFollowing = false;

            await _progressIndicatorService.HideAsync();
        }


        private void Reset()
        {
            IsFollowing = false;
            Activities.Clear();
            Repositories.Clear();
        }

        public async Task LoadUserDataAsync()
        {
            IsFollowing = await ViewModelLocator.GitHubService.IsFollowingAsync(User.Login);

            var activities = await ViewModelLocator.GitHubService.GetUserActivitiesAsync(User.Login);
            foreach (var activity in activities)
            {
                Activities.Add(activity);
            }

            var repositories = await ViewModelLocator.GitHubService.GetUserRepositoriesAsync(User.Login);
            foreach (var repository in repositories)
            {
                Repositories.Add(repository);
            }
        }
    }
}
