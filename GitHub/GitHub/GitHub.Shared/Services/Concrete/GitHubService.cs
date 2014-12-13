using System.Collections.Generic;
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
    }
}