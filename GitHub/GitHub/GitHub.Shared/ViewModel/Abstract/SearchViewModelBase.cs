using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;

namespace GitHub.ViewModel.Abstract
{
    public abstract class SearchViewModelBase : ReactiveViewModelBase, ISearchViewModel
    {
        private string _searchValue;
        public string SearchValue
        {
            get { return _searchValue; }
            set
            {
                _searchValue = value;
                this.RaisePropertyChanged();
            }
        }

        public ReactiveCommand<Unit> Search { get; protected set; }


        protected SearchViewModelBase()
        {
            // Search part
            CreateSearchCommand();
        }


        private async Task CreateSearchCommand()
        {
            var canSearch = this.WhenAny(x => x.SearchValue, x => !string.IsNullOrWhiteSpace(x.Value));
            Search = ReactiveCommand.CreateAsyncTask(canSearch, async _ => await Refresh());

            Search.ThrownExceptions
                .Subscribe(ex => UserError.Throw("Potential Network Connectivity Error", ex));

            this.WhenAnyValue(x => x.SearchValue)
                .Throttle(TimeSpan.FromSeconds(1), RxApp.MainThreadScheduler)
                .InvokeCommand(this, x => x.Search);
        }
        public abstract Task Refresh();
    }
}
