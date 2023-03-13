using MessageSenderAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MessageSenderAPI.Services.Background
{
    public class EmailBackgroundService : BackgroundService
    {
        private const int delay = 1 * 60 * 1000;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public EmailBackgroundService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("Background work!");
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _emailService = scope.ServiceProvider.GetService<IEmailService>();
                    await _emailService.CheckToSendMessagesAsync();
                }
                Console.WriteLine("Background completed. Wait 1 minute to work again!");
                await Task.Delay(delay, stoppingToken);
            }
        }
    }
}
