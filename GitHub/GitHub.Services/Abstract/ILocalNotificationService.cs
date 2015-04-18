namespace GitHub.Services.Abstract
{
    public interface ILocalNotificationService
    {
        void SendNotification(string title, string content);
    }
}
