using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using GitHub.Services.Abstract;
using Octokit;

namespace GitHub.Services.Concrete
{
    public class GlobalGitHubService : IGitHubService
    {
        protected readonly IGitHubClient _client;


        public GlobalGitHubService(IGitHubClient client)
        {
            _client = client;
        }


        #region Authentication

        public string AccessToken
        {
            get { return (string)(ApplicationData.Current.LocalSettings.Values["token"]); }
            set { ApplicationData.Current.LocalSettings.Values["token"] = value; }
        }

        public void TryAuthenticate(OauthToken token = null)
        {
            if (token != null)
                AccessToken = token.AccessToken;

            _client.Connection.Credentials = new Credentials(AccessToken);
        }

        #endregion

        #region Get single item

        public virtual async Task<User> GetCurrentUserAsync()
        {
            return await _client.User.Current();
        }

        public virtual async Task<User> GetUserAsync(string name)
        {
            return await _client.User.Get(name);
        }

        #endregion

        #region Get multiple items

        public virtual async Task<IReadOnlyList<Activity>> GetActivitiesAsync()
        {
            return await _client.Activity.Events.GetAll();
        }

        public virtual async Task<IReadOnlyList<Activity>> GetUserActivitiesAsync(string user)
        {
            var performedActivities = await _client.Activity.Events.GetUserPerformed(user);
            var receivedActivities = await _client.Activity.Events.GetUserReceived(user);

            return performedActivities.Concat(receivedActivities).OrderByDescending(a => a.CreatedAt).ToArray();
        }

        public virtual async Task<IReadOnlyList<Repository>> GetUserRepositoriesAsync(string user)
        {
            return await _client.Repository.GetAllForUser(user);
        }

        public virtual async Task<IReadOnlyList<GitHubCommit>> GetRepositoryCommitsAsync(string owner, string repository)
        {
            return await _client.Repository.Commits.GetAll(owner, repository);
        }

        public virtual async Task<IReadOnlyList<Issue>> GetRepositoryIssuesAsync(string owner, string repository)
        {
            return await _client.Issue.GetForRepository(owner, repository);
        }

        #endregion

        #region Current user related data

        public virtual async Task<IReadOnlyList<User>> GetCurrentFollowersAsync()
        {
            return await _client.User.Followers.GetAllForCurrent();
        }

        public virtual async Task<IReadOnlyList<User>> GetCurrentFollowingsAsync()
        {
            return await _client.User.Followers.GetFollowingForCurrent();
        }

        public virtual async Task<IReadOnlyList<Repository>> GetCurrentPublicReposAsync()
        {
            var repositories = await _client.Repository.GetAllForCurrent();
            return repositories.Where(r => !r.Private).ToArray();
        }

        public virtual async Task<IReadOnlyList<Repository>> GetCurrentPrivateReposAsync()
        {
            var repositories = await _client.Repository.GetAllForCurrent();
            return repositories.Where(r => r.Private).ToArray();
        }

        public virtual async Task<IReadOnlyList<Gist>> GetCurrentGistsAsync()
        {
            return await _client.Gist.GetAll();
        }

        #endregion

        #region Search items

        public virtual async Task<SearchUsersResult> SearchUsersAsync(string searchName, int page = 1, int elementPerPage = 100)
        {
            return await _client.Search.SearchUsers(new SearchUsersRequest(searchName)
            {
                Page = page,
                PerPage = elementPerPage
            });
        }

        public virtual async Task<SearchRepositoryResult> SearchReposAsync(string searchName, int page = 1, int elementPerPage = 100)
        {
            return await _client.Search.SearchRepo(new SearchRepositoriesRequest(searchName)
            {
                Page = page,
                PerPage = elementPerPage
            });
        }

        #endregion

        #region Actions

        public virtual async Task<bool> IsFollowingAsync(string user)
        {
            return await _client.User.Followers.IsFollowingForCurrent(user);
        }

        public virtual async Task<bool> FollowUserAsync(string user)
        {
            return await _client.User.Followers.Follow(user);
        }

        public virtual async Task UnfollowUserAsync(string user)
        {
            await _client.User.Followers.Unfollow(user);
        }

        #endregion

        #region Tree management

        public virtual async Task<TreeResponse> GetRepositoryTreeAsync(string owner, string repository, string reference)
        {
            return await _client.GitDatabase.Tree.Get(owner, repository, reference);
        }

        #endregion

        #region Notifications

        public virtual async Task<IReadOnlyList<Notification>> GetCurrentNotificationsAsync(DateTimeOffset lastCheck)
        {
            return await _client.Notification.GetAllForCurrent(new NotificationsRequest { Since = lastCheck });
        }

        #endregion
    }
}
