using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.EventLog;

using Serilog;

using CecEmulator.DomainModels;
using CecEmulator.Services;

const string ServiceName = "Roku Wake Service";

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger());

builder.Services.AddWindowsService(options => {
    options.ServiceName = ServiceName;
});

builder.Services.Configure<DefaultRokuOptions>(
    builder.Configuration.GetSection(nameof(DefaultRokuOptions)));

builder.Services.AddSingleton<Func<SsdpLocatorService>>((serviceProvider) => serviceProvider.GetRequiredService<SsdpLocatorService>);
builder.Services.AddTransient<SsdpLocatorService>();

builder.Services.AddHttpClient<RokuService>();

builder.Services.AddHostedService<WindowsBackgroundService>();

LoggerProviderOptions
    .RegisterProviderOptions<EventLogSettings, EventLogLoggerProvider>(builder.Services);

// See: https://github.com/dotnet/runtime/issues/47303
builder.Logging.AddConfiguration(
    builder.Configuration.GetSection(nameof(builder.Logging)));

IHost host = builder.Build();
host.Run();