using GitHub.DataObjects.Concrete;

namespace GitHub.ViewModel.Abstract
{
    public interface IReposViewModel
    {
        ReposIncrementalLoadingCollection Repositories { get; }
    }
}