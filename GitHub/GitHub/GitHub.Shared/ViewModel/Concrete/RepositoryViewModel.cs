using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GitHub.Services.Abstract;
using GitHub.ViewModel.Abstract;
using Octokit;
using GalaSoft.MvvmLight.Command;

namespace GitHub.ViewModel.Concrete
{
    public class RepositoryViewModel : ViewModelBase, IRepositoryViewModel
    {
        #region Services

        private readonly INavigationService _navigationService;
        private readonly IProgressIndicatorService _progressIndicatorService;

        #endregion


        #region Properties

        private Repository _repository;
        public Repository Repository
        {
            get { return _repository; }
            set
            {
                _repository = value;
                RaisePropertyChanged();
                Reset();
            }
        }

        private readonly ObservableCollection<TreeItem> _treeItems = new ObservableCollection<TreeItem>();
        public ObservableCollection<TreeItem> TreeItems { get { return _treeItems; } }

        private readonly ObservableCollection<GitHubCommit> _commits = new ObservableCollection<GitHubCommit>();
        public ObservableCollection<GitHubCommit> Commits { get { return _commits; } }

        private readonly ObservableCollection<Issue> _issues = new ObservableCollection<Issue>();
        public ObservableCollection<Issue> Issues { get { return _issues; } }

        public string CurrentTopFolderSha { get; private set; }

        private readonly IList<string> _topFoldersSha = new List<string>();
        public IList<string> TopFoldersSha { get { return _topFoldersSha; } }

        #endregion


        #region Commands

        public ICommand SelectTreeItemCommand { get; private set; }
        public ICommand GoBackTreeCommand { get; private set; }
        public ICommand SelectCommitCommand { get; private set; }
        public ICommand SelectIssueCommand { get; private set; }

        #endregion


        #region Constructor

        public RepositoryViewModel(INavigationService navigationService,
            IProgressIndicatorService progressIndicatorService)
        {
            _navigationService = navigationService;
            _progressIndicatorService = progressIndicatorService;

            SelectTreeItemCommand = new RelayCommand<TreeItem>(SelectTreeItem);
            GoBackTreeCommand = new RelayCommand(GoBackTree);
            SelectCommitCommand = new RelayCommand<GitHubCommit>(SelectCommit);
            SelectIssueCommand = new RelayCommand<Issue>(SelectIssue);

            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data

                var odonno = new User("https://github.com/identicons/odonno.png",
                    null, null, 100, null, new DateTimeOffset(), 0, null, 144, 3, null, null, 0,
                    1, null,
                    "Odonno", "David Bottiau",
                    0, null, 0, 0, 44, null, false);

                var odonnoAuthor = new Author("Odonno", 1, "https://github.com/identicons/odonno.png",
                   null, null, null, null, null, "User", null, null, null, null, null, null, false);

                Repository = new Repository(null, null, null, null, null, null, null,
                    1,
                    odonno,
                    "First-Repository",
                    "Odonno/First-Repository",
                    "A new description of the current repository",
                    null, null,
                    false, false,
                    0, 0, 0,
                    "master",
                    0,
                    null, new DateTimeOffset(), new DateTimeOffset(),
                    null, null, null, null,
                    false, false, false);

                var firstCommit = new GitHubCommit(null, null,
                    "4c66460", "4c66460e677296dc1e3229a70c3d3db39b795d10",
                    odonno, Repository, odonnoAuthor,
                    null,
                    new Commit(null, null, null, null, odonno, Repository, "Fix bugs",
                         null, null, null, new List<GitReference>(), 10),
                    odonnoAuthor, null,
                    new GitHubCommitStats(22, 22, 0),
                    null, null);

                var longCommit = new GitHubCommit(null, null,
                    "4c66460", "4c66460e677296dc1e3229a70c3d3db39b795d10",
                    odonno, Repository, odonnoAuthor,
                    null,
                    new Commit(null, null, null, null, odonno, Repository, "This is a long long long commit !",
                        null, null, null, new List<GitReference>(), 10),
                    odonnoAuthor, null,
                    new GitHubCommitStats(44, 34, 10),
                    null, null);

                var veryLongCommit = new GitHubCommit(null, null,
                 "4c66460", "4c66460e677296dc1e3229a70c3d3db39b795d10",
                 odonno, Repository, odonnoAuthor,
                 null,
                 new Commit(null, null, null, null, odonno, Repository, "This is a very very very very long long " + Environment.NewLine + Environment.NewLine +
                                                                        "long long long long commit !",
                     null, null, null, new List<GitReference>(), 10),
                 odonnoAuthor, null,
                 new GitHubCommitStats(44, 34, 10),
                 null, null);


                Commits.Add(firstCommit);
                Commits.Add(longCommit);
                Commits.Add(veryLongCommit);

                var gitignore = new TreeItem(".gitignore", null, TreeType.Blob, 1, null, null);
                var github = new TreeItem("GitHub", null, TreeType.Tree, 10, null, null);
                var license = new TreeItem("LICENSE", null, TreeType.Blob, 5, null, null);
                var readme = new TreeItem("README.md", null, TreeType.Blob, 40, null, null);

                TreeItems.Add(gitignore);
                TreeItems.Add(github);
                TreeItems.Add(license);
                TreeItems.Add(readme);


                var firstIssue = new Issue(null, null, 1, ItemState.Open,
                    "Opened issue", "The long body of an issue !",
                    odonno, null, null, new Milestone(0), 2, null, null, new DateTimeOffset(), null);

                var closedIssue = new Issue(null, null, 1, ItemState.Closed,
                    "Closed issue", "The long body of an issue !",
                    odonno, null, null, new Milestone(0), 2, null, null, new DateTimeOffset(), null);

                Issues.Add(firstIssue);
                Issues.Add(closedIssue);
                Issues.Add(firstIssue);
                Issues.Add(closedIssue);
                Issues.Add(firstIssue);
            }
            else
            {
                // Code runs "for real"
            }
        }

        #endregion


        #region Command methods

        private async void SelectTreeItem(TreeItem treeItem)
        {
            if (treeItem.Type == TreeType.Tree)
            {
                await _progressIndicatorService.ShowAsync();

                // replace the top folder to go back recursively
                CurrentTopFolderSha = TopFoldersSha.Last();

                // add the node (top folder) to access it after
                TopFoldersSha.Add(treeItem.Sha);

                // create the new tree
                await CreateTree(treeItem.Sha);

                await _progressIndicatorService.HideAsync();

                return;
            }

            if (treeItem.Type == TreeType.Blob)
            {
                // TODO : create a specific page
                _navigationService.NavigateTo("InDevelopment");
                return;
            }

            throw new NotImplementedException();
        }

        private async void GoBackTree()
        {
            await _progressIndicatorService.ShowAsync();

            // remove the node (current top folder) cause it is useless now
            TopFoldersSha.Remove(TopFoldersSha.Last());
            
            // replace the top folder to go back recursively
            CurrentTopFolderSha = TopFoldersSha.Last();

            // create the new tree
            await CreateTree(CurrentTopFolderSha);

            await _progressIndicatorService.HideAsync();
        }

        private void SelectCommit(GitHubCommit commit)
        {
            // TODO : create a specific page
            _navigationService.NavigateTo("InDevelopment");
        }

        private void SelectIssue(Issue issue)
        {
            // TODO : create a specific page
            _navigationService.NavigateTo("InDevelopment");
        }

        #endregion


        #region Methods

        private void Reset()
        {
            CurrentTopFolderSha = null;
            TopFoldersSha.Clear();

            TreeItems.Clear();
            Commits.Clear();
            Issues.Clear();
        }

        public async Task LoadRepositoryDataAsync()
        {
            // TODO : load repository data
            var commits = await ViewModelLocator.GitHubService.GetRepositoryCommitsAsync(Repository.Owner.Login, Repository.Name);
            foreach (var commit in commits)
            {
                Commits.Add(commit);
            }

            var lastCommit = Commits.FirstOrDefault();
            if (lastCommit != null)
            {
                TopFoldersSha.Add(lastCommit.Sha);
                await CreateTree(lastCommit.Sha);
            }

            var issues = await ViewModelLocator.GitHubService.GetRepositoryIssuesAsync(Repository.Owner.Login, Repository.Name);
            foreach (var issue in issues)
            {
                Issues.Add(issue);
            }
        }

        private async Task CreateTree(string sha)
        {
            TreeItems.Clear();

            // add an access to top folder (if it is possible)
            if (TopFoldersSha.Count > 1)
                TreeItems.Add(new TreeItem("..", null, new TreeType(), 0, CurrentTopFolderSha, null));

            var treeItems = await ViewModelLocator.GitHubService.GetRepositoryTreeAsync(Repository.Owner.Login, Repository.Name, sha);
            foreach (var treeItem in treeItems.Tree)
                TreeItems.Add(treeItem);
        }

        #endregion
    }
}
