namespace GitHub.ViewModel.Abstract
{
    public interface IMainViewModel
    {
        IProfileViewModel ProfileViewModel { get; }
        IActivitiesViewModel ActivitiesViewModel { get; }
        IReposViewModel ReposViewModel { get; }
        IUsersViewModel UsersViewModel { get; }
    }
}