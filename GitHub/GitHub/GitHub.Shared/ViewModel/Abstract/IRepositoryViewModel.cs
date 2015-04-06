using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Octokit;

namespace GitHub.ViewModel.Abstract
{
    public interface IRepositoryViewModel
    {
        Repository Repository { get; set; }
        ObservableCollection<TreeItem> TreeItems { get; }
        ObservableCollection<GitHubCommit> Commits { get; }
        ObservableCollection<Issue> Issues { get; }

        string CurrentTopFolderSha { get; }
        IList<string> TopFoldersSha { get; }

        ICommand SelectTreeItemCommand { get; }
        ICommand GoBackTreeCommand { get; }
        ICommand SelectCommitCommand { get; }
        ICommand SelectIssueCommand { get; }

        Task LoadRepositoryDataAsync();
    }
}
