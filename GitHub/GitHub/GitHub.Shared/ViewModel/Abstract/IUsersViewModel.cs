using System.Collections.ObjectModel;
using Octokit;
using ReactiveUI;

namespace GitHub.ViewModel.Abstract
{
    public interface IUsersViewModel
    {
        ObservableCollection<User> Users { get; }
        string SearchValue { get; set; }
        
        ReactiveCommand<SearchUsersResult> Search { get; }
    }
}