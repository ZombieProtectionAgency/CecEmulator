using System.Xml.Serialization;

using Microsoft.Extensions.Options;

using LanguageExt;
using LanguageExt.Common;
using Rssdp;

using CecEmulator.DomainModels;

namespace CecEmulator.Services;

public class RokuService
{ 
    private const short OneSecondInMs = 1000;

    public bool HasService => Service != null;

    private Uri? Service;

    private readonly ILogger<RokuService> _logger;
    private readonly HttpClient _httpClient;
    private readonly Func<SsdpLocatorService> _rokuLocatorFactory;
    private readonly IOptionsMonitor<DefaultRokuOptions> _options;

    public RokuService(
        ILogger<RokuService> logger,
        HttpClient httpClient,
        Func<SsdpLocatorService> rokuLocatorFactory,
        IOptionsMonitor<DefaultRokuOptions> options) => 
        (_httpClient, _options, _logger, _rokuLocatorFactory) = (httpClient, options, logger, rokuLocatorFactory);

    public async Task LocateServiceAsync() 
    {
        using var locator = _rokuLocatorFactory.Invoke();

        await locator.Discover();

        foreach (var device in locator.Devices) 
        {
            SsdpDevice info;

            try 
            {
                info = await device.GetDeviceInfo();
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, $"Error locating device info for {device.Usn}");
                continue;
            }

            if (_options.CurrentValue.DeviceName.Equals(info.FriendlyName)) {
                Service = device.DescriptionLocation;
                _logger.LogInformation($"Discovered {device.Usn}");
                break;
            }
        }
    }

    public TryAsync<Unit> LaunchAppAsync() => async () =>
    {
        await GetDeviceInfoAsync().IfSucc(async deviceInfo => {
            var shouldPowerOn = deviceInfo.Powermode == "Ready";

            // Dont care if this succeeded or not, lets try to switch inputs regardless.
            // Waiting just in case it takes a second to wake up, not sure if I need this.
            await WakeDevice();
            await Task.Delay(OneSecondInMs);
		});

		return await LaunchApp().Try();
    };

    private TryAsync<Unit> WakeDevice() => async () =>
	{
        var requestUrl = $"{Service}launch/{_options.CurrentValue.AppName}";
		await _httpClient.PostAsync(requestUrl, null);
		return Unit.Default;
    };

    private TryAsync<Unit> LaunchApp() => async () =>
	{
        var requestUrl = $"{Service}launch/{_options.CurrentValue.AppName}";
		await _httpClient.PostAsync(requestUrl, null);
		return Unit.Default;
    };

    private TryAsync<DeviceInfo> GetDeviceInfoAsync() => async () =>
    {
        var requestUrl = $"{Service}query/device-info";

		var request = await _httpClient.GetAsync(requestUrl);

		var responseXml = await request.Content.ReadAsStringAsync();
		var serializer = new XmlSerializer(typeof(DeviceInfo));

		using var reader = new StringReader(responseXml);
		var result = serializer.Deserialize(reader) as DeviceInfo;

        if (result == null) return new Result<DeviceInfo>(Error.New("Could not deserialize."));

        return new Result<DeviceInfo>(result);
    };
}
