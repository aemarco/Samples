using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using WpfGenericHost.View;

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

        MainWindow = _host.Services.GetRequiredService<MainWindow>();
        MainWindow.Show();
    }

    private void App_OnExit(object sender, ExitEventArgs e)
    {
        _host?.Dispose();
    }
}
