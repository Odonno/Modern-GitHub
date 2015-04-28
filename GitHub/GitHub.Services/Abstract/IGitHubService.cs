using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Octokit;

namespace GitHub.Services.Abstract
{
    public interface IGitHubService
    {
        #region Authentication

        string AccessToken { get; }

        void TryAuthenticate(OauthToken token = null);

        #endregion

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

        #region Current user related data

        Task<IReadOnlyList<User>> GetCurrentFollowersAsync();
        Task<IReadOnlyList<User>> GetCurrentFollowingsAsync();
        Task<IReadOnlyList<Repository>> GetCurrentPublicReposAsync();
        Task<IReadOnlyList<Repository>> GetCurrentPrivateReposAsync();
        Task<IReadOnlyList<Gist>> GetCurrentGistsAsync();

        #endregion

        #region Search items

        Task<SearchUsersResult> SearchUsersAsync(string searchName, int page = 1, int elementPerPage = 100);
        Task<SearchRepositoryResult> SearchReposAsync(string searchName, int page = 1, int elementPerPage = 100);

        #endregion

        #region Actions

        Task<bool> IsFollowingAsync(string user);
        Task<bool> FollowUserAsync(string user);
        Task UnfollowUserAsync(string user);

        #endregion

        #region Tree management

        Task<TreeResponse> GetRepositoryTreeAsync(string owner, string repository, string reference);

        #endregion

        #region Notifications

        Task<IReadOnlyList<Notification>> GetCurrentNotificationsAsync(DateTimeOffset lastCheck);

        #endregion
    }
}
