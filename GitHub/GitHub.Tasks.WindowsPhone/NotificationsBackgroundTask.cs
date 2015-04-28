using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Storage;
using GitHub.Services.Abstract;
using GitHub.Services.Concrete;
using Octokit;

namespace GitHub.Tasks.WindowsPhone
{
    public sealed class NotificationsBackgroundTask : IBackgroundTask
    {
        #region Services

        private readonly ILocalNotificationService _localNotificationService;
        private readonly IGitHubService _gitHubService;

        #endregion


        #region Fields

        private BackgroundTaskDeferral _deferral;

        #endregion


        #region Properties

        public DateTimeOffset LastCheckNotificationsDate
        {
            get { return (DateTimeOffset)(ApplicationData.Current.RoamingSettings.Values["lastCheckNotificationDate"] ?? DateTimeOffset.Now); }
            set { ApplicationData.Current.RoamingSettings.Values["lastCheckNotificationDate"] = value; }
        }

        #endregion


        #region Constructor

        public NotificationsBackgroundTask()
        {
            _localNotificationService = new LocalNotificationService();

            var client = new GitHubClient(new ProductHeaderValue("UniversalGitHub"));
            _gitHubService = new GlobalGitHubService(client);
        }

        #endregion


        #region Methods

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            _deferral = taskInstance.GetDeferral();
            await Do();
        }

        private async Task Do()
        {
            try
            {
                // you need to be authenticated first to get current notifications
                _gitHubService.TryAuthenticate();

                // get new notifications
                var notifications = await _gitHubService.GetCurrentNotificationsAsync(LastCheckNotificationsDate);

                // reset last time we get notifications
                LastCheckNotificationsDate = DateTime.Now;

                foreach (var notification in notifications)
                {
                    // show notifications (toast + badge notifications)
                    string notificationContent = string.Format("{0} ({1})", notification.Subject.Title, notification.Repository.Name);
                    _localNotificationService.SendNotification(notification.Subject.Type, notificationContent);
                }
            }
            finally
            {
                _deferral.Complete();
            }
        }

        #endregion
    }
}
