using Windows.UI.Notifications;
using GitHub.Services.Abstract;

namespace GitHub.Services.Concrete
{
    public class ToastNotificationService : IToastNotificationService
    {
        public void SendNotification(string title, string content)
        {
            // get toast notifier
            var toastNotifier = ToastNotificationManager.CreateToastNotifier();

            // create notification form
            const ToastTemplateType toastTemplate = ToastTemplateType.ToastText02; 
            var toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);

            var toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(title));
            toastTextElements[1].AppendChild(toastXml.CreateTextNode(content));

            // create the notification from the template before
            var toastNotification = new ToastNotification(toastXml);

            // show notif
            toastNotifier.Show(toastNotification);
        }
    }
}
