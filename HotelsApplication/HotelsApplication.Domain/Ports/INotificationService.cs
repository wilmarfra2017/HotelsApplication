namespace HotelsApplication.Domain.Ports
{
    public interface INotificationService
    {
        Task SendAsync(string recipient, string subject, string message);        
    }
}
