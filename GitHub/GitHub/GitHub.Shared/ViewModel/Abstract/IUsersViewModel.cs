using System.Reactive;
using GitHub.DataObjects.Concrete;
using ReactiveUI;

namespace GitHub.ViewModel.Abstract
{
    public interface IUsersViewModel
    {
        UsersIncrementalLoadingCollection Users { get; }
        string SearchValue { get; set; }

        ReactiveCommand<Unit> Search { get; }
    }
}