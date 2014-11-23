using System.Collections.Generic;
using Octokit;
using System.Threading.Tasks;

namespace GitHub.Core
{
    public interface IGitHubManager
    {
        #region Get items

        Task<User> GetCurrentUser();
        Task<User> GetUserAsync(string name);
        Task GetNews();

        #endregion


        #region Search items

        Task<SearchUsersResult> SearchUsers(string searchName);
        Task<SearchRepositoryResult> SearchRepos(string searchName);

        #endregion
    }
}
