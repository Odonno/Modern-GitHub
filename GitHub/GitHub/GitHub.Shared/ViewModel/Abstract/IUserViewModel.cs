using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Octokit;

namespace GitHub.ViewModel.Abstract
{
    public interface IUserViewModel
    {
        User User { get; set; }
        bool IsFollowing { get; }
        bool CanFollow { get; }
        ObservableCollection<Activity> Activities { get; }
        ObservableCollection<Repository> Repositories { get; }

        ICommand FollowCommand { get; }
        ICommand UnfollowCommand { get; }


        Task LoadUserDataAsync();
    }
}
