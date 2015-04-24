using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Octokit;
using GitHub.Services.Abstract;

namespace GitHub.Services.Concrete
{
    public class GitHubService : GlobalGitHubService
    {
        private readonly ILocalNotificationService _localNotificationService;


        public GitHubService(IGitHubClient client, ILocalNotificationService localNotificationService)
            : base(client)
        {
            _localNotificationService = localNotificationService;
        }


        #region Get single item

        public override async Task<User> GetCurrentUserAsync()
        {
            try
            {
                return await base.GetCurrentUserAsync();
            }
            catch (Exception ex)
            {
                _localNotificationService.SendNotification("An error occured", "Can't retrieve your profile");
                App.TelemetryClient.TrackException(ex);
                return null;
            }
        }

        public override async Task<User> GetUserAsync(string name)
        {
            try
            {
                return await base.GetUserAsync(name);
            }
            catch (NotFoundException ex)
            {
                _localNotificationService.SendNotification("An error occured", string.Format("Can't retrieve user {0}", name));
                App.TelemetryClient.TrackException(ex);
                return null;
            }
        }

        #endregion

        #region Get multiple items

        public override async Task<IReadOnlyList<Activity>> GetActivitiesAsync()
        {
            try
            {
                return await base.GetActivitiesAsync();
            }
            catch (Exception ex)
            {
                _localNotificationService.SendNotification("An error occured", "Can't retrieve global activities");
                App.TelemetryClient.TrackException(ex);
                return new Activity[] { };
            }
        }

        public override async Task<IReadOnlyList<Activity>> GetUserActivitiesAsync(string user)
        {
            try
            {
                return await base.GetUserActivitiesAsync(user);
            }
            catch (Exception ex)
            {
                _localNotificationService.SendNotification("An error occured", string.Format("Can't retrieve activities of user {0}", user));
                App.TelemetryClient.TrackException(ex);
                return new Activity[] { };
            }
        }

        public override async Task<IReadOnlyList<Repository>> GetUserRepositoriesAsync(string user)
        {
            try
            {
                return await base.GetUserRepositoriesAsync(user);
            }
            catch (Exception ex)
            {
                _localNotificationService.SendNotification("An error occured", string.Format("Can't retrieve repositories of user {0}", user));
                App.TelemetryClient.TrackException(ex);
                return new Repository[] { };
            }
        }

        public override async Task<IReadOnlyList<GitHubCommit>> GetRepositoryCommitsAsync(string owner, string repository)
        {
            try
            {
                return await base.GetRepositoryCommitsAsync(owner, repository);
            }
            catch (Exception ex)
            {
                _localNotificationService.SendNotification("An error occured", string.Format("Can't retrieve commits of repository {0}", repository));
                App.TelemetryClient.TrackException(ex);
                return new GitHubCommit[] { };
            }
        }

        public override async Task<IReadOnlyList<Issue>> GetRepositoryIssuesAsync(string owner, string repository)
        {
            try
            {
                return await base.GetRepositoryIssuesAsync(owner, repository);
            }
            catch (Exception ex)
            {
                _localNotificationService.SendNotification("An error occured", string.Format("Can't retrieve issues of repository {0}", repository));
                App.TelemetryClient.TrackException(ex);
                return new Issue[] { };
            }
        }

        #endregion

        #region Current user related data

        public override async Task<IReadOnlyList<User>> GetCurrentFollowersAsync()
        {
            try
            {
                return await base.GetCurrentFollowersAsync();
            }
            catch (Exception ex)
            {
                _localNotificationService.SendNotification("An error occured", "Can't retrieve your followers");
                App.TelemetryClient.TrackException(ex);
                return new User[] { };
            }
        }

        public override async Task<IReadOnlyList<User>> GetCurrentFollowingsAsync()
        {
            try
            {
                return await base.GetCurrentFollowingsAsync();
            }
            catch (Exception ex)
            {
                _localNotificationService.SendNotification("An error occured", "Can't retrieve your followings");
                App.TelemetryClient.TrackException(ex);
                return new User[] { };
            }
        }

        public override async Task<IReadOnlyList<Repository>> GetCurrentPublicReposAsync()
        {
            try
            {
                return await base.GetCurrentPublicReposAsync();
            }
            catch (Exception ex)
            {
                _localNotificationService.SendNotification("An error occured", "Can't retrieve your public repositories");
                App.TelemetryClient.TrackException(ex);
                return new Repository[] { };
            }
        }

        public override async Task<IReadOnlyList<Repository>> GetCurrentPrivateReposAsync()
        {
            try
            {
                return await base.GetCurrentPrivateReposAsync();
            }
            catch (Exception ex)
            {
                _localNotificationService.SendNotification("An error occured", "Can't retrieve your private repositories");
                App.TelemetryClient.TrackException(ex);
                return new Repository[] { };
            }
        }

        public override async Task<IReadOnlyList<Gist>> GetCurrentGistsAsync()
        {
            try
            {
                return await base.GetCurrentGistsAsync();
            }
            catch (Exception ex)
            {
                _localNotificationService.SendNotification("An error occured", "Can't retrieve your gists");
                App.TelemetryClient.TrackException(ex);
                return new Gist[] { };
            }
        }

        #endregion

        #region Search items

        public override async Task<SearchUsersResult> SearchUsersAsync(string searchName, int page = 1, int elementPerPage = 100)
        {
            try
            {
                return await base.SearchUsersAsync(searchName, page, elementPerPage);
            }
            catch (Exception ex)
            {
                _localNotificationService.SendNotification("An error occured", string.Format("Can't complete user search for {0}", searchName));
                App.TelemetryClient.TrackException(ex);
                return new SearchUsersResult();
            }
        }

        public override async Task<SearchRepositoryResult> SearchReposAsync(string searchName, int page = 1, int elementPerPage = 100)
        {
            try
            {
                return await base.SearchReposAsync(searchName, page, elementPerPage);
            }
            catch (Exception ex)
            {
                _localNotificationService.SendNotification("An error occured", string.Format("Can't complete repo search for {0}", searchName));
                App.TelemetryClient.TrackException(ex);
                return new SearchRepositoryResult();
            }
        }

        #endregion

        #region Actions

        public override async Task<bool> IsFollowingAsync(string user)
        {
            try
            {
                return await base.IsFollowingAsync(user);
            }
            catch (Exception ex)
            {
                _localNotificationService.SendNotification("An error occured", string.Format("Can't get if you follow {0}", user));
                App.TelemetryClient.TrackException(ex);
                return false;
            }
        }
        public override async Task<bool> FollowUserAsync(string user)
        {
            try
            {
                return await base.FollowUserAsync(user);
            }
            catch (Exception ex)
            {
                _localNotificationService.SendNotification("An error occured", string.Format("Can't follow {0}", user));
                App.TelemetryClient.TrackException(ex);
                return false;
            }
        }
        public override async Task UnfollowUserAsync(string user)
        {
            try
            {
                await base.UnfollowUserAsync(user);
            }
            catch (Exception ex)
            {
                _localNotificationService.SendNotification("An error occured", string.Format("Can't unfollow {0}", user));
                App.TelemetryClient.TrackException(ex);
            }
        }

        #endregion

        #region Tree management

        public override async Task<TreeResponse> GetRepositoryTreeAsync(string owner, string repository, string reference)
        {
            try
            {
                return await base.GetRepositoryTreeAsync(owner, repository, reference);
            }
            catch (Exception ex)
            {
                _localNotificationService.SendNotification("An error occured", string.Format("Can't get the tree of repo {0}", repository));
                App.TelemetryClient.TrackException(ex);
                return new TreeResponse();
            }
        }

        #endregion

        #region Notifications

        public override async Task<IReadOnlyList<Notification>> GetCurrentNotificationsAsync(DateTimeOffset lastCheck)
        {
            try
            {
                return await base.GetCurrentNotificationsAsync(lastCheck);
            }
            catch (Exception ex)
            {
                _localNotificationService.SendNotification("An error occured", "Can't get the notifications");
                App.TelemetryClient.TrackException(ex);
                return new Notification[] { };
            }
        }

        #endregion
    }
}