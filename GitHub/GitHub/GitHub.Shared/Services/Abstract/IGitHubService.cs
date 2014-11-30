using System.Threading.Tasks;
using Octokit;

namespace GitHub.Services.Abstract
{
    public interface IGitHubService
    {
        #region Get items

        Task<User> GetCurrentUserAsync();
        Task<User> GetUserAsync(string name);
        Task GetActivitiesAsync();

        #endregion


        #region Search items

        Task<SearchUsersResult> SearchUsersAsync(string searchName);
        Task<SearchRepositoryResult> SearchReposAsync(string searchName);

        #endregion
    }
}
