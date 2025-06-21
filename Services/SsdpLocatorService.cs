using System.Collections.Concurrent;
using System.Net;
using System.Net.NetworkInformation;

using Rssdp;

namespace CecEmulator.Services;

public class SsdpLocatorService : IDisposable
{
    private readonly BlockingCollection<DiscoveredSsdpDevice> _discoveredDevices = new();
    private readonly List<Rssdp.SsdpDeviceLocator> _locators = new ();

    private CancellationTokenSource? _cancellationTokenSource;
    private bool disposedValue;

    private readonly ILogger<SsdpLocatorService> _logger;

    public SsdpLocatorService(
        ILogger<SsdpLocatorService> logger) =>
        (_logger) = (logger);

    public IEnumerable<DiscoveredSsdpDevice> Devices => 
        _discoveredDevices.GetConsumingEnumerable(_cancellationTokenSource?.Token ?? throw new InvalidOperationException("Must start discovery before enumerating devices."));

    public async Task Discover()
    {
        _cancellationTokenSource = new CancellationTokenSource();

        await Parallel.ForEachAsync(GetLocalInterfaces(), _cancellationTokenSource.Token, DiscoverDevicesOnInterface);
    }

    private async ValueTask DiscoverDevicesOnInterface(IPAddress address, CancellationToken cancellationToken)
    {
        // Just in case the parallelizer gets to this after a device was already located, maybe pointless?
        if (cancellationToken.IsCancellationRequested) return; 

        var deviceLocator = new Rssdp.SsdpDeviceLocator(address.ToString());
        _locators.Add(deviceLocator);

        deviceLocator.DeviceAvailable += DeviceDiscovered;

        deviceLocator.StartListeningForNotifications();

        // Request reannounce manually so we dont have to wait for a passive alive ping.
        await deviceLocator.SearchAsync();
    }

    private void DeviceDiscovered(object? sender, DeviceAvailableEventArgs e) 
    {
        if (e.IsNewlyDiscovered) {
            _discoveredDevices.Add(e.DiscoveredDevice);
        }
    }

    private IEnumerable<IPAddress> GetLocalInterfaces() {
        var addresses = NetworkInterface
            .GetAllNetworkInterfaces()
            .Where(n => n.OperationalStatus == OperationalStatus.Up)
            .Where(n => n.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || n.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
            .SelectMany(n => n.GetIPProperties()?.UnicastAddresses ?? Enumerable.Empty<UnicastIPAddressInformation>())
            .Where(n => n.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            .Select(g => g.Address)
            .ToArray();

        _logger.LogInformation($"Found {addresses.Count()} network interfaces.");

        return addresses;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                foreach (var locator in _locators) {
                    locator.Dispose();
                }
            }

            _cancellationTokenSource?.Cancel();
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}