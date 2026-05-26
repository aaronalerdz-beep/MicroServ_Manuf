namespace Cycle.QUERY.DOMAIN.Services
{
    public interface IWebNotificationService
    {
        Task SendUpdateAsync(string subject,object data );
    }
}