using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Foundation;
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


        public abstract IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count);
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
