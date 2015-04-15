using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Storage;
using GitHub.Services.Abstract;
using GitHub.ViewModel;

namespace GitHub.Tasks
{
    public class NotificationsBackgroundTask : IBackgroundTask
    {
        #region Services

        private readonly IToastNotificationService _toastNotificationService;

        #endregion


        #region Fields

        private BackgroundTaskDeferral _deferral;

        #endregion


        #region Properties

        public DateTime LastCheckNotificationsDate
        {
            get { return (DateTime) (ApplicationData.Current.RoamingSettings.Values["lastCheckNotificationDate"] ?? DateTime.Now); }
            set { ApplicationData.Current.RoamingSettings.Values["lastCheckNotificationDate"] = value; }
        }

        #endregion


        #region Constructor

        public NotificationsBackgroundTask(IToastNotificationService toastNotificationService)
        {
            _toastNotificationService = toastNotificationService;
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
                // BUG : you need to be authenticated to get current notifications => retrieve/save Token
                // get new notifications
                var notifications = await ViewModelLocator.GitHubService.GetCurrentNotificationsAsync(new DateTimeOffset(LastCheckNotificationsDate));

                // reset last time we get notifications
                LastCheckNotificationsDate = DateTime.Now;

                foreach (var notification in notifications)
                {
                    // show notifications (toast notifications)
                    string notificationContent = string.Format("{0} ({1})", notification.Subject.Title, notification.Repository.Name);
                    _toastNotificationService.SendNotification(notification.Subject.Type, notificationContent);

                    // TODO : show notifications (badge notifications)

                }
            }
            catch (Exception ex)
            {
                // send a metric for any exception on this background task
                App.TelemetryClient.TrackException(ex);
            }
            finally
            {
                _deferral.Complete();
            }
        }

        #endregion
    }
}
