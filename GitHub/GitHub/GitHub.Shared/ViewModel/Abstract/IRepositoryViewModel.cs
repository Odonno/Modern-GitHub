using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Octokit;

namespace GitHub.ViewModel.Abstract
{
    public interface IRepositoryViewModel
    {
        Repository Repository { get; set; }
        ObservableCollection<TreeItem> TreeItems { get; }
        ObservableCollection<GitHubCommit> Commits { get; }

        Task LoadRepositoryDataAsync();
    }
}
