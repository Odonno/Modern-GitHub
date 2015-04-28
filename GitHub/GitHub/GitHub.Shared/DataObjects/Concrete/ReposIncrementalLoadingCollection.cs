using System.Collections.Generic;
using System.Threading.Tasks;
using GitHub.DataObjects.Abstract;
using GitHub.ViewModel;
using Octokit;

namespace GitHub.DataObjects.Concrete
{
    public class ReposIncrementalLoadingCollection : IncrementalLoadingCollection<Repository>
    {
        public ReposIncrementalLoadingCollection()
        {
            ItemsPerPage = 30;
        }

        protected async override Task<IReadOnlyList<Repository>> LoadMoreItemsAsync()
        {
            var result = await ViewModelLocator.GitHubService.SearchReposAsync(SearchValue, ++Page, ItemsPerPage);
            TotalCount = result.TotalCount;
            return result.Items;
        }
    }
}
