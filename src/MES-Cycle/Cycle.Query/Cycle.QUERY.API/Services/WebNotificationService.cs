using Cycle.QUERY.DOMAIN.Services;
using Microsoft.AspNetCore.SignalR;
using Cycle.QUERY.API.SignalR;

namespace Cycle.QUERY.API.Services
{
    public class WebNotificationService : IWebNotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public WebNotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendUpdateAsync(string subject, object data)
        {
            await _hubContext.Clients.All.SendAsync(subject, data);
        }

    }
}