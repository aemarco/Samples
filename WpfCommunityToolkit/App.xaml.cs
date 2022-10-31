using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace WpfCommunityToolkit;

public partial class App
{
    private IHost? _host;
    private void App_OnStartup(object sender, StartupEventArgs e)
    {

        _host = Host.CreateDefaultBuilder(e.Args)
            .ConfigureLogging(b =>
            {
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .Enrich.FromLogContext()
                    .Enrich.WithThreadId()
                    .Enrich.WithMachineName()
                    .Enrich.WithEnvironmentUserName()
                    .WriteTo.Debug()
                    .CreateLogger();

                b.ClearProviders();
                b.AddSerilog();
            })
            .ConfigureServices((_, services) =>
            {
                services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);
                services.AddTransient<MainWindowViewModel>();
                services.AddTransient<MainWindow>();

            })
            .Build();
        _host.Start();

        MainWindow = _host.Services.GetRequiredService<MainWindow>();
        MainWindow.Show();
    }

    private void App_OnExit(object sender, ExitEventArgs e)
    {
        _host?.StopAsync().GetAwaiter().GetResult();
        _host?.Dispose();
    }
}
