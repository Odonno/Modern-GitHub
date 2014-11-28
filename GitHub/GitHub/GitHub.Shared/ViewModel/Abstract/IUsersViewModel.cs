using System.Collections.ObjectModel;
using System.Windows.Input;
using Octokit;

namespace GitHub.ViewModel.Abstract
{
    public interface IUsersViewModel
    {
        ObservableCollection<User> Users { get; }

        ICommand SearchCommand { get; }
    }
}