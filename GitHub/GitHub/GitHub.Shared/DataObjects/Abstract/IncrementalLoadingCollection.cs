using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace GitHub.DataObjects.Abstract
{
    public abstract class IncrementalLoadingCollection<T> : ObservableCollection<T>, ISupportIncrementalLoading, ISearchCollection
    {
        public bool HasMoreItems { get { return Page * ItemsPerPage < TotalCount; } }
        public string SearchValue { get; set; }
        public int Page { get; protected set; }
        public int TotalCount { get; protected set; }
        public int ItemsPerPage { get; protected set; }
        public bool IsBusy { get; protected set; }


        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            if (IsBusy)
                throw new InvalidOperationException("Only one operation in flight at a time");
            IsBusy = true;

            try
            {
                CoreDispatcher dispatcher = Window.Current.Dispatcher;

                return Task.Run(
                    async () =>
                    {
                        var items = await LoadMoreItemsAsync();

                        dispatcher.RunAsync(
                            CoreDispatcherPriority.High,
                            () =>
                            {
                                foreach (var item in items)
                                    Add(item);
                            });

                        return new LoadMoreItemsResult { Count = (uint)items.Count };
                    }).AsAsyncOperation();
            }
            finally
            {
                IsBusy = false;
            }
        }
        protected abstract Task<IReadOnlyList<T>> LoadMoreItemsAsync();
        public void Reset(string searchValue = null)
        {
            if (searchValue != null)
                SearchValue = searchValue;
            Clear();
            Page = 0;
        }
    }
}
