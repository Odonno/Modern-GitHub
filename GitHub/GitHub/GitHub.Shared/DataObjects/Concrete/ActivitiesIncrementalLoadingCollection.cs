using System.Collections.Generic;
using System.Threading.Tasks;
using GitHub.DataObjects.Abstract;
using GitHub.Services.Abstract;
using Microsoft.Practices.ServiceLocation;
using Octokit;

namespace GitHub.DataObjects.Concrete
{
    public class ActivitiesIncrementalLoadingCollection : IncrementalLoadingCollection<Activity>
    {
        public ActivitiesIncrementalLoadingCollection()
        {
            ItemsPerPage = 30;
        }

        protected async override Task<IReadOnlyList<Activity>> LoadMoreItemsAsync()
        {
            var items = await ServiceLocator.Current.GetInstance<IGitHubService>().GetActivitiesAsync();
            TotalCount = items.Count;
            return items;
        }
    }
}
