using System.Collections.Generic;
using System.Threading.Tasks;
using GitHub.DataObjects.Abstract;
using GitHub.Services.Abstract;
using Microsoft.Practices.ServiceLocation;
using Octokit;

namespace GitHub.DataObjects.Concrete
{
    public class ReposIncrementalLoadingCollection : IncrementalLoadingCollection<Repository>
    {
        public ReposIncrementalLoadingCollection()
        {
            ItemsPerPage = 40;
        }

        protected async override Task<IReadOnlyList<Repository>> LoadMoreItemsAsync()
        {
            var result = await ServiceLocator.Current.GetInstance<IGitHubService>().SearchReposAsync(SearchValue, ++Page, ItemsPerPage);
            TotalCount = result.TotalCount;
            return result.Items;
        }
    }
}
