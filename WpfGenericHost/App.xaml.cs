using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace WpfGenericHost;

public partial class App
{
    private IHost? _host;
    private void App_OnStartup(object sender, StartupEventArgs e)
    {
        _host = Host.CreateDefaultBuilder(e.Args)
            .ConfigureServices((_, services) =>
            {
                services.AddTransient<MainWindow>();

            })
            .Build();
        _host.Start();

        var window = _host.Services.GetRequiredService<MainWindow>();
        window.Show();
    }

    private void App_OnExit(object sender, ExitEventArgs e)
    {
        _host?.Dispose();
    }
}
