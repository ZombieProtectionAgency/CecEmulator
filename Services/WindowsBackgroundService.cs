using LanguageExt;

namespace CecEmulator.Services;

// https://learn.microsoft.com/en-us/dotnet/core/extensions/windows-service?pivots=dotnet-7-0
public class WindowsBackgroundService : BackgroundService
{
    private static readonly TimeSpan InactiveTime = new (0, 10, 0);
    private static readonly TimeSpan RecentlyAwokenTime = new (0, 10, 0);

    private DateTime _lastWakeupTime = DateTime.Now;
    private TimeSpan _previousIdleTime = default;

    private readonly ILogger<WindowsBackgroundService> _logger;
    private readonly RokuService _rokuService;

    public WindowsBackgroundService(
        ILogger<WindowsBackgroundService> logger, 
        RokuService rokuService) => 
        (_logger, _rokuService) = (logger, rokuService);
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            if (!_rokuService.HasService) {
                await _rokuService.LocateServiceAsync();
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                var idleTime = TimeSpan.FromMilliseconds(IdleTimeFinder.GetIdleTime());
                var isInactive = idleTime > InactiveTime;
                var hasRecentlyWoken = DateTime.Now - _lastWakeupTime <= RecentlyAwokenTime;
                var hasBecomeActive = idleTime < _previousIdleTime;

                if (hasBecomeActive && !isInactive && !hasRecentlyWoken)
                {
                    _logger.LogWarning("Device Waking Up!");

                    await _rokuService.LaunchAppAsync().IfFail(ex => {
                        _logger.LogError(ex, "Failed to launch app.");
                        return Unit.Default;
                    });

                    _lastWakeupTime = DateTime.Now;
                }

                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);

                _previousIdleTime = idleTime;
            }
        }
        catch (TaskCanceledException)
        {
            _logger.LogInformation("Service Stopped.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Message}", ex.Message);

            Environment.Exit(1);
        }
    }
}
