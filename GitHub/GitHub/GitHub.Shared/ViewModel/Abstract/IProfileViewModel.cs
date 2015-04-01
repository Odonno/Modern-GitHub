using System.Threading.Tasks;
using System.Windows.Input;
using Octokit;

namespace GitHub.ViewModel.Abstract
{
    public interface IProfileViewModel
    {
        User CurrentUser { get; }

        ICommand GoToFollowersCommand { get; }
        ICommand GoToFollowingsCommand { get; }
        ICommand GoToCollaboratorsCommand { get; }
        ICommand GoToPublicReposCommand { get; }  
        ICommand GoToPublicGistsCommand { get; }  
        ICommand GoToPrivateReposCommand { get; }

        Task LoadAsync();
    }
}