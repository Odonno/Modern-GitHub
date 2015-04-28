using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using GitHub.Services.Abstract;

namespace GitHub.Services.Concrete
{
    public class LocalNotificationService : ILocalNotificationService
    {
        public void SendNotification(string title, string content)
        {
            // Send any notification (Tile, Toast, Badge)
            SendToastNotification(title, content);
        }


        private void SendToastNotification(string title, string content)
        {
            // get toast notifier
            var toastNotifier = ToastNotificationManager.CreateToastNotifier();

            // create notification form
            XmlDocument toastXml;

            if (string.IsNullOrWhiteSpace(title))
            {
                toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);

                var toastTextElements = toastXml.GetElementsByTagName("text");
                toastTextElements[0].AppendChild(toastXml.CreateTextNode(content));
            }
            else
            {
                toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);

                var toastTextElements = toastXml.GetElementsByTagName("text");
                toastTextElements[0].AppendChild(toastXml.CreateTextNode(title));
                toastTextElements[1].AppendChild(toastXml.CreateTextNode(content));
            }

            // create the notification from the template before
            var toastNotification = new ToastNotification(toastXml);

            // show notif
            toastNotifier.Show(toastNotification);
        }
        

        private void SendBadgeUpdate(int value)
        {
            // get badge updater
            var badgeUpdater = BadgeUpdateManager.CreateBadgeUpdaterForApplication();

            // create badge form
            var badgeXml = BadgeUpdateManager.GetTemplateContent(BadgeTemplateType.BadgeNumber);
            var badgeElement = ((XmlElement)badgeXml.SelectSingleNode("/badge"));
            badgeElement.SetAttribute("value", value.ToString());

            // create the notification from the template before
            var badgeNotification = new BadgeNotification(badgeXml);

            // show badge
            badgeUpdater.Update(badgeNotification);
        }
    }
}
