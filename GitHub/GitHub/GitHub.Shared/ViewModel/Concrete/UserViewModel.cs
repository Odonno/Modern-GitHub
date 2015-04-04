using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GitHub.Common;
using GitHub.ViewModel.Abstract;
using Octokit;

namespace GitHub.ViewModel.Concrete
{
    public class UserViewModel : ViewModelBase, IUserViewModel
    {
        private User _user;
        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                RaisePropertyChanged();
                Reset();
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

        private readonly ObservableCollection<Activity> _activities = new ObservableCollection<Activity>();
        public ObservableCollection<Activity> Activities { get { return _activities; } }

        private readonly ObservableCollection<Repository> _repositories = new ObservableCollection<Repository>();
        public ObservableCollection<Repository> Repositories { get { return _repositories; } }


        public ICommand FollowCommand { get; private set; }
        public ICommand UnfollowCommand { get; private set; }


        public UserViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data
                
                //Repositories.Add(new Repository { Name = "First-Repository", FullName = "Odonno/First-Repository" });
                //Repositories.Add(new Repository { Name = "Another-Repository", FullName = "Odonno/Another-Repository" });

                //Activities.Add(new Activity
                //{
                //    Actor = User,
                //    CreatedAt = new DateTimeOffset(new DateTime(2014, 12, 12)),
                //    Public = true,
                //    Repo = Repositories.First(),
                //    Type = "PushEvent"
                //});
                //Activities.Add(new Activity
                //{
                //    Actor = User,
                //    CreatedAt = new DateTimeOffset(new DateTime(2014, 12, 11)),
                //    Public = true,
                //    Repo = Repositories.First(),
                //    Type = "CreateEvent"
                //});
            }
            else
            {
                // Code runs "for real"

                FollowCommand = new RelayCommand(Follow);
                UnfollowCommand = new RelayCommand(Unfollow);
            }
        }


        private async void Follow()
        {
            await ViewModelLocator.GitHubService.FollowUser(User.Login);
            IsFollowing = true;
        }

        private async void Unfollow()
        {
            await ViewModelLocator.GitHubService.UnfollowUser(User.Login);
            IsFollowing = false;
        }


        private void Reset()
        {
            IsFollowing = false;
            Activities.Clear();
            Repositories.Clear();
        }

        public async Task LoadUserDataAsync()
        {
            IsFollowing = await ViewModelLocator.GitHubService.IsFollowing(User.Login);

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
