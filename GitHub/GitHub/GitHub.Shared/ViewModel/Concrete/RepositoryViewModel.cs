using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GitHub.ViewModel.Abstract;
using Octokit;

namespace GitHub.ViewModel.Concrete
{
    public class RepositoryViewModel : ViewModelBase, IRepositoryViewModel
    {
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

        private readonly ObservableCollection<GitHubCommit> _commits = new ObservableCollection<GitHubCommit>();
        public ObservableCollection<GitHubCommit> Commits { get { return _commits; } }


        public RepositoryViewModel()
        {
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
                    null, null, null,
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
            }
            else
            {
                // Code runs "for real"
            }
        }


        private void Reset()
        {
            Commits.Clear();
        }

        public async Task LoadRepositoryDataAsync()
        {
            // TODO : load repository data
            var commits = await ViewModelLocator.GitHubService.GetRepositoryCommitsAsync(Repository.Owner.Login, Repository.Name);
            foreach (var commit in commits)
            {
                Commits.Add(commit);
            }
        }
    }
}
