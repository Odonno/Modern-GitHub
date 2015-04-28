using System.Collections.Generic;
using System.Threading.Tasks;
using GitHub.DataObjects.Abstract;
using GitHub.ViewModel;
using Octokit;

namespace GitHub.DataObjects.Concrete
{
    public class UsersIncrementalLoadingCollection : IncrementalLoadingCollection<User>
    {
        public UsersIncrementalLoadingCollection()
        {
            ItemsPerPage = 30;
        }

        protected async override Task<IReadOnlyList<User>> LoadMoreItemsAsync()
        {
            var result = await ViewModelLocator.GitHubService.SearchUsersAsync(SearchValue, ++Page, ItemsPerPage);
            TotalCount = result.TotalCount;
            return result.Items;
        }
    }
}
