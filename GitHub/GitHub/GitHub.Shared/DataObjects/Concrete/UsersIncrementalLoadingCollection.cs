using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using GitHub.DataObjects.Abstract;
using GitHub.ViewModel;
using Octokit;

namespace GitHub.DataObjects.Concrete
{
    public class UsersIncrementalLoadingCollection : IncrementalLoadingCollection<User>
    {
        public UsersIncrementalLoadingCollection()
        {
            ItemsPerPage = 40;
        }

        public override IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
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

        protected async override Task<IReadOnlyList<User>> LoadMoreItemsAsync()
        {
            var result = await ViewModelLocator.GitHubService.SearchUsersAsync(SearchValue, ++Page, ItemsPerPage);
            TotalCount = result.TotalCount;
            return result.Items;
        }
    }
}
