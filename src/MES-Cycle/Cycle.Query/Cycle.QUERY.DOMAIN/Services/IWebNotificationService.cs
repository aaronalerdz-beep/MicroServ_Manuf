namespace Cycle.QUERY.DOMAIN.Services
{
    public interface IWebNotificationService
    {
        Task SendUpdateAsync(object data);
    }
}