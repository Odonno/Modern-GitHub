using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Octokit;

namespace GitHub.ViewModel.Abstract
{
    public interface IRepositoryViewModel
    {
        Repository Repository { get; set; }
        ObservableCollection<GitHubCommit> Commits { get; }

        Task LoadRepositoryDataAsync();
    }
}
