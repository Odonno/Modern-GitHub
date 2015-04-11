using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;
using Octokit;
using GitHub.Services.Abstract;

namespace GitHub.Services.Concrete
{
    public class GitHubService : IGitHubService
    {
        private readonly IGitHubClient _client;

        public GitHubService()
        {
            _client = ServiceLocator.Current.GetInstance<IGitHubClient>();
        }

        #region Get single item

        public async Task<User> GetCurrentUserAsync()
        {
            return await _client.User.Current();
        }

        public async Task<User> GetUserAsync(string name)
        {
            try
            {
                return await _client.User.Get(name);
            }
            catch (NotFoundException)
            {
                return null;
            }
        }

        #endregion

        #region Get multiple items

        public async Task<IReadOnlyList<Activity>> GetActivitiesAsync()
        {
            return await _client.Activity.Events.GetAll();
        }

        public async Task<IReadOnlyList<Activity>> GetUserActivitiesAsync(string user)
        {
            var performedActivities = await _client.Activity.Events.GetUserPerformed(user);
            var receivedActivities = await _client.Activity.Events.GetUserReceived(user);

            return performedActivities.Concat(receivedActivities).OrderByDescending(a => a.CreatedAt).ToArray();
        }

        public async Task<IReadOnlyList<Repository>> GetUserRepositoriesAsync(string user)
        {
            return await _client.Repository.GetAllForUser(user);
        }

        public async Task<IReadOnlyList<GitHubCommit>> GetRepositoryCommitsAsync(string owner, string repository)
        {
            return await _client.Repository.Commits.GetAll(owner, repository);
        }

        public async Task<IReadOnlyList<Issue>> GetRepositoryIssuesAsync(string owner, string repository)
        {
            return await _client.Issue.GetForRepository(owner, repository);
        }
        
        #endregion

        #region Current user related data

        public async Task<IReadOnlyList<User>> GetCurrentCollaborators()
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<User>> GetCurrentFollowers()
        {
            return await _client.User.Followers.GetAllForCurrent();
        }

        public async Task<IReadOnlyList<User>> GetCurrentFollowings()
        {
            return await _client.User.Followers.GetFollowingForCurrent();
        }

        public async Task<IReadOnlyList<Repository>> GetCurrentPublicRepos()
        {
            var repositories = await _client.Repository.GetAllForCurrent();
            return repositories.Where(r => !r.Private).ToArray();
        }

        public async Task<IReadOnlyList<Repository>> GetCurrentPrivateRepos()
        {
            var repositories = await _client.Repository.GetAllForCurrent();
            return repositories.Where(r => r.Private).ToArray();
        }

        public async Task<IReadOnlyList<Gist>> GetCurrentGists()
        {
            return await _client.Gist.GetAll();
        }

        #endregion

        #region Search items

        public async Task<SearchUsersResult> SearchUsersAsync(string searchName, int page = 1, int elementPerPage = 100)
        {
            return await _client.Search.SearchUsers(new SearchUsersRequest(searchName)
            {
                Page = page,
                PerPage = elementPerPage
            });
        }

        public async Task<SearchRepositoryResult> SearchReposAsync(string searchName, int page = 1, int elementPerPage = 100)
        {
            return await _client.Search.SearchRepo(new SearchRepositoriesRequest(searchName)
            {
                Page = page,
                PerPage = elementPerPage
            });
        }

        #endregion

        #region Actions

        public async Task<bool> IsFollowing(string user)
        {
            return await _client.User.Followers.IsFollowingForCurrent(user);
        }
        public async Task<bool> FollowUser(string user)
        {
            return await _client.User.Followers.Follow(user);
        }
        public async Task UnfollowUser(string user)
        {
            await _client.User.Followers.Unfollow(user);
        }

        #endregion

        #region Tree management

        public async Task<TreeResponse> GetRepositoryTree(string owner, string repository, string reference)
        {
            return await _client.GitDatabase.Tree.Get(owner, repository, reference);
        }

        #endregion
    }
}