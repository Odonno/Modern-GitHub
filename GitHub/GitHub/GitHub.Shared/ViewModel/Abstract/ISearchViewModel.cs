using System.Reactive;
using ReactiveUI;

namespace GitHub.ViewModel.Abstract
{
    public interface ISearchViewModel
    {
        string SearchValue { get; set; }

        ReactiveCommand<Unit> Search { get; }
    }
}
