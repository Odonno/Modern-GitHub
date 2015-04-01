using System.Collections.Generic;
using System.Threading.Tasks;
using Octokit;

namespace GitHub.Services.Abstract
{
    public interface IGitHubService
    {
        #region Get items

        Task<User> GetCurrentUserAsync();
        Task<User> GetUserAsync(string name);

        #endregion

        #region Get multiple items

        Task<IReadOnlyList<Activity>> GetActivitiesAsync();
        Task<IReadOnlyList<Activity>> GetUserActivitiesAsync(string user);

        #endregion

        #region Search items

        Task<SearchUsersResult> SearchUsersAsync(string searchName, int page = 1, int elementPerPage = 100);
        Task<SearchRepositoryResult> SearchReposAsync(string searchName, int page = 1, int elementPerPage = 100);

        #endregion
    }
}
