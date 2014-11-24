using System.Threading.Tasks;
using Octokit;

namespace GitHub.Core.Abstract
{
    public interface IGitHubManager
    {
        #region Get items

        Task<User> GetCurrentUser();
        Task<User> GetUserAsync(string name);
        Task GetActivities();

        #endregion


        #region Search items

        Task<SearchUsersResult> SearchUsers(string searchName);
        Task<SearchRepositoryResult> SearchRepos(string searchName);

        #endregion
    }
}
