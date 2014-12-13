using GitHub.DataObjects.Concrete;

namespace GitHub.ViewModel.Abstract
{
    public interface IUsersViewModel
    {
        UsersIncrementalLoadingCollection Users { get; }
    }
}