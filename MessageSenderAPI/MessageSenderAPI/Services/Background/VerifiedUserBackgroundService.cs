using MessageSenderAPI.Services.Interfaces;

namespace MessageSenderAPI.Services.Background
{
    public class VerifiedUserBackgroundService : BackgroundService
    {
        private const int delay = 3 * 60 * 1000;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public VerifiedUserBackgroundService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("VerifiedUserBackgroundService work!");
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _userService = scope.ServiceProvider.GetService<IUserService>();
                    await _userService.CheckUnverifiedUsersAsync();                    
                }
                Console.WriteLine("VerifiedUserBackgroundService completed. Wait 3 minutes to work again!");
                await Task.Delay(delay, stoppingToken);
            }
        }
    }
}
