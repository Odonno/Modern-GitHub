using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Octokit;

namespace GitHub.ViewModel.Abstract
{
    public interface IProfileViewModel
    {
        User CurrentUser { get; }

        ObservableCollection<User> Followers { get; }
        ObservableCollection<User> Followings { get; }
        ObservableCollection<User> Collaborators { get; }
        ObservableCollection<Repository> PublicRepos { get; }
        ObservableCollection<Repository> PrivateRepos { get; }
        ObservableCollection<Gist> Gists { get; }

        ICommand GoToFollowersCommand { get; }
        ICommand GoToFollowingsCommand { get; }
        ICommand GoToCollaboratorsCommand { get; }
        ICommand GoToPublicReposCommand { get; }
        ICommand GoToPrivateReposCommand { get; }
        ICommand GoToGistsCommand { get; }
        ICommand GoToGistCommand { get; }

        Task LoadAsync();
    }
}