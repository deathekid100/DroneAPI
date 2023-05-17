using DronesAPI.Services;

namespace DronesAPI.Jobs
{
    public class AuditJob : BackgroundService
    {
        private readonly PeriodicTimer _timer = new(TimeSpan.FromMinutes(1));
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public AuditJob(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (await _timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("Audit");
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var auditService = scope.ServiceProvider.GetRequiredService<IAuditService>();
                    await auditService.DroneBatteryLevelsReport();
                }
            }
        }
    }
}
