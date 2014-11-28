using System.Collections.ObjectModel;
using System.Windows.Input;
using Octokit;

namespace GitHub.ViewModel.Abstract
{
    public interface IReposViewModel
    {
        ObservableCollection<Repository> Repositories { get; }

        ICommand SearchCommand { get; }
    }
}