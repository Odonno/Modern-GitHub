using System;
using System.Reactive;
using System.Reactive.Linq;
using GalaSoft.MvvmLight.Ioc;
using GitHub.DataObjects.Concrete;
using GitHub.ViewModel.Abstract;
using Octokit;
using ReactiveUI;

namespace GitHub.ViewModel.Concrete
{
    public class ReposViewModel : ReactiveViewModelBase, IReposViewModel
    {
        public ReposIncrementalLoadingCollection Repositories { get; private set; }

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

        public ReactiveCommand<Unit> Search { get; private set; }


        public ReposViewModel()
        {
            Repositories = SimpleIoc.Default.GetInstance<ReposIncrementalLoadingCollection>();

            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.

                Repositories.Add(new Repository { Name = "First Repository" });
                Repositories.Add(new Repository { Name = "Another Repository" });
            }
            else
            {
                // Code runs "for real"

                // TODO : first request on last created repos ?

                // Search part
                var canSearch = this.WhenAny(x => x.SearchValue, x => !string.IsNullOrWhiteSpace(x.Value));
                Search = ReactiveCommand.CreateAsyncTask(canSearch, async _ =>
                {
                    Repositories.Reset(SearchValue);
                    await Repositories.LoadMoreItemsAsync((uint)Repositories.ItemsPerPage);
                });

                Search.ThrownExceptions
                    .Subscribe(ex => UserError.Throw("Potential Network Connectivity Error", ex));

                this.WhenAnyValue(x => x.SearchValue)
                    .Throttle(TimeSpan.FromSeconds(1), RxApp.MainThreadScheduler)
                    .InvokeCommand(this, x => x.Search);
            }
        }
    }
}