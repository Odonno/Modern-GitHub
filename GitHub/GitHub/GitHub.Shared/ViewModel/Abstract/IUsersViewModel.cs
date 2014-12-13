using System.Windows.Input;
using GitHub.DataObjects.Concrete;

namespace GitHub.ViewModel.Abstract
{
    public interface IUsersViewModel
    {
        UsersIncrementalLoadingCollection Users { get; }

        ICommand GoToUserCommand { get; }
    }
}