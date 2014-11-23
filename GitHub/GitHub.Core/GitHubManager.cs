using System;
using Octokit;
using System.Threading.Tasks;

namespace GitHub.Core
{
    public class GitHubManager : IGitHubManager
    {
        private readonly GitHubClient _client;

        public GitHubManager()
        {
            _client = new GitHubClient(new ProductHeaderValue("UniversalGitHub"));
        }
        
        #region Get single item

        public Task<User> GetCurrentUser()
        {
            throw new NotImplementedException();
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

        public Task GetNews()
        {
            throw new NotImplementedException();
        }

        #endregion
        
        #region Search items

        public async Task<SearchUsersResult> SearchUsers(string searchName)
        {
            return await _client.Search.SearchUsers(new SearchUsersRequest(searchName));
        }

        public async Task<SearchRepositoryResult> SearchRepos(string searchName)
        {
            return await _client.Search.SearchRepo(new SearchRepositoriesRequest(searchName));
        }

        #endregion
    }
}