namespace GitHub.Services.Abstract
{
    public interface IToastNotificationService
    {
        void SendNotification(string title, string content);
    }
}
