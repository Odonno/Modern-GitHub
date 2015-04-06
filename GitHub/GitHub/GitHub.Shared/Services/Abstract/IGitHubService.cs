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
        Task<IReadOnlyList<Repository>> GetUserRepositoriesAsync(string user);
        Task<IReadOnlyList<GitHubCommit>> GetRepositoryCommitsAsync(string owner, string repository);
        Task<IReadOnlyList<Issue>> GetRepositoryIssuesAsync(string owner, string repository);

        #endregion

        #region Search items

        Task<SearchUsersResult> SearchUsersAsync(string searchName, int page = 1, int elementPerPage = 100);
        Task<SearchRepositoryResult> SearchReposAsync(string searchName, int page = 1, int elementPerPage = 100);

        #endregion

        #region Actions

        Task<bool> IsFollowing(string user);
        Task<bool> FollowUser(string user);
        Task UnfollowUser(string user);

        #endregion

        #region Tree management

        Task<TreeResponse> GetRepositoryTree(string owner, string repository, string reference);

        #endregion
    }
}
