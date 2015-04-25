using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using GitHub.Services.Abstract;
using GitHub.ViewModel.Abstract;
using Octokit;

namespace GitHub.ViewModel.Concrete
{
    public class ProfileViewModel : ViewModelBase, IProfileViewModel
    {
        #region Services

        private readonly INavigationService _navigationService;
        private readonly IProgressIndicatorService _progressIndicatorService;

        #endregion


        #region Properties

        public User CurrentUser { get; private set; }

        private readonly ObservableCollection<User> _followers = new ObservableCollection<User>();
        public ObservableCollection<User> Followers { get { return _followers; } }

        private readonly ObservableCollection<User> _followings = new ObservableCollection<User>();
        public ObservableCollection<User> Followings { get { return _followings; } }

        private readonly ObservableCollection<User> _collaborators = new ObservableCollection<User>();
        public ObservableCollection<User> Collaborators { get { return _collaborators; } }

        private readonly ObservableCollection<Repository> _publicRepos = new ObservableCollection<Repository>();
        public ObservableCollection<Repository> PublicRepos { get { return _publicRepos; } }

        private readonly ObservableCollection<Repository> _privateRepos = new ObservableCollection<Repository>();
        public ObservableCollection<Repository> PrivateRepos { get { return _privateRepos; } }

        private readonly ObservableCollection<Gist> _gists = new ObservableCollection<Gist>();
        public ObservableCollection<Gist> Gists { get { return _gists; } }

        #endregion


        #region Commands

        public ICommand GoToFollowersCommand { get; private set; }
        public ICommand GoToFollowingsCommand { get; private set; }
        public ICommand GoToCollaboratorsCommand { get; private set; }
        public ICommand GoToPublicReposCommand { get; private set; }
        public ICommand GoToPrivateReposCommand { get; private set; }
        public ICommand GoToGistsCommand { get; private set; }
        public ICommand GoToGistCommand { get; private set; }

        #endregion


        #region Constructor

        public ProfileViewModel(INavigationService navigationService,
            IProgressIndicatorService progressIndicatorService)
        {
            _navigationService = navigationService;
            _progressIndicatorService = progressIndicatorService;

            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.

                CurrentUser = new User("https://github.com/identicons/odonno.png",
                    null, null, 100, null, new DateTimeOffset(), 0, null, 144, 3, null, null, 0,
                    1, null,
                    "Odonno", "David Bottiau",
                    0, null, 0, 0, 44, null, false);


                var firstRepo = new Repository(null, null, null, null, null, null, null,
                    1,
                    null,
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
                    null,
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

                PublicRepos.Add(firstRepo);
                PublicRepos.Add(anotherRepo);
                PublicRepos.Add(firstRepo);
                PublicRepos.Add(anotherRepo);

                PrivateRepos.Add(firstRepo);
                PrivateRepos.Add(anotherRepo);
                PrivateRepos.Add(firstRepo);
                PrivateRepos.Add(anotherRepo);


                var odonno = new User("https://github.com/identicons/odonno.png",
                    null, null, 100, null, new DateTimeOffset(), 0, null, 144, 3, null, null, 0,
                    1, null,
                    "Odonno", "David Bottiau",
                    0, null, 0, 0, 44, null, false);

                var corentinMiq = new User("https://github.com/identicons/CorentinMiq.png",
                    null, null, 200, null, new DateTimeOffset(), 0, null, 7, 84, null, null, 0,
                    2, null,
                    "CorentinMiq", "Corentin Miquée",
                    0, null, 0, 0, 3, null, false);

                Collaborators.Add(odonno);
                Collaborators.Add(corentinMiq);
                Collaborators.Add(odonno);
                Collaborators.Add(corentinMiq);

                Followers.Add(odonno);
                Followers.Add(corentinMiq);
                Followers.Add(odonno);
                Followers.Add(corentinMiq);

                Followings.Add(odonno);
                Followings.Add(corentinMiq);
                Followings.Add(odonno);
                Followings.Add(corentinMiq);


                var firstGist = new Gist(null, "1", "A first gist in C#", true, CurrentUser,
                   new Dictionary<string, GistFile>
                    {
                        { "test.cs", new GistFile(0, "test.cs", null, "C#", null, null) }
                    },
                    4, null, null, null, null,
                    new DateTimeOffset(), new DateTimeOffset(),
                    new List<GistFork>(), new List<GistHistory>());

                var secondGist = new Gist(null, "2", "A second gist in another languages", true, CurrentUser,
                    new Dictionary<string, GistFile>
                    {
                        { "test.cpp", new GistFile(0, "test.cpp", null, "C++", null, null) },
                        { "test.vb", new GistFile(0, "test.vb", null, "Visual Basic", null, null) }
                    },
                    4, null, null, null, null,
                    new DateTimeOffset(), new DateTimeOffset(),
                    new List<GistFork>(), new List<GistHistory>());

                Gists.Add(firstGist);
                Gists.Add(secondGist);
                Gists.Add(firstGist);
                Gists.Add(secondGist);
            }
            else
            {
                // Code runs "for real"

                GoToFollowersCommand = new RelayCommand(GoToFollowers);
                GoToFollowingsCommand = new RelayCommand(GoToFollowings);
                GoToCollaboratorsCommand = new RelayCommand(GoToCollaborators);
                GoToPublicReposCommand = new RelayCommand(GoToPublicRepos);
                GoToPrivateReposCommand = new RelayCommand(GoToPrivateRepos);
                GoToGistsCommand = new RelayCommand(GoToGists);
                GoToGistCommand = new RelayCommand<Gist>(GoToGist);
            }
        }

        #endregion


        #region Methods

        public async Task LoadAsync()
        {
            await _progressIndicatorService.ShowAsync();

            CurrentUser = await ViewModelLocator.GitHubService.GetCurrentUserAsync();
            RaisePropertyChanged("CurrentUser");

            await _progressIndicatorService.HideAsync();
        }

        #endregion


        #region Command methods

        private async void GoToFollowers()
        {
            await _progressIndicatorService.ShowAsync();

            var followers = await ViewModelLocator.GitHubService.GetCurrentFollowersAsync();

            Followers.Clear();
            foreach (var follower in followers)
                Followers.Add(follower);

            _navigationService.NavigateTo("MyFollowers");

            await _progressIndicatorService.HideAsync();
        }

        private async void GoToFollowings()
        {
            await _progressIndicatorService.ShowAsync();

            var followings = await ViewModelLocator.GitHubService.GetCurrentFollowingsAsync();

            Followings.Clear();
            foreach (var following in followings)
                Followings.Add(following);

            _navigationService.NavigateTo("MyFollowings");

            await _progressIndicatorService.HideAsync();
        }

        private void GoToCollaborators()
        {
            // TODO : get the collaborator list
            _navigationService.NavigateTo("InDevelopment");
        }

        private async void GoToPublicRepos()
        {
            await _progressIndicatorService.ShowAsync();

            var publicRepos = await ViewModelLocator.GitHubService.GetCurrentPublicReposAsync();

            PublicRepos.Clear();
            foreach (var repo in publicRepos)
                PublicRepos.Add(repo);

            _navigationService.NavigateTo("MyPublicRepos");

            await _progressIndicatorService.HideAsync();
        }

        private async void GoToPrivateRepos()
        {
            await _progressIndicatorService.ShowAsync();

            var privateRepos = await ViewModelLocator.GitHubService.GetCurrentPrivateReposAsync();

            PrivateRepos.Clear();
            foreach (var repo in privateRepos)
                PrivateRepos.Add(repo);

            _navigationService.NavigateTo("MyPrivateRepos");

            await _progressIndicatorService.HideAsync();
        }

        private async void GoToGists()
        {
            await _progressIndicatorService.ShowAsync();

            // BUG : Files's GistFiles throw EntryPointNotFoundException (not expected)
            var gists = await ViewModelLocator.GitHubService.GetCurrentGistsAsync();

            Gists.Clear();
            foreach (var gist in gists)
                Gists.Add(gist);

            _navigationService.NavigateTo("MyGists");

            await _progressIndicatorService.HideAsync();
        }

        private void GoToGist(Gist gist)
        {
            // TODO : implement new page
            _navigationService.NavigateTo("InDevelopment");
        }

        #endregion
    }
}