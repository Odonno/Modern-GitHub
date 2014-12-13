using System.Reactive;
using GitHub.DataObjects.Concrete;
using ReactiveUI;

namespace GitHub.ViewModel.Abstract
{
    public interface IReposViewModel
    {
        ReposIncrementalLoadingCollection Repositories { get; }

        ReactiveCommand<Unit> Search { get; }
    }
}