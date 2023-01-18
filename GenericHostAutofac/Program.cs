using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


var hb = Host.CreateDefaultBuilder(args)
        .ConfigureHostConfiguration(_ =>
        {

        })
        .ConfigureAppConfiguration((_, c) =>
        {
            c.AddJsonFile("file.json", true);
        })
        .UseServiceProviderFactory(new AutofacServiceProviderFactory()) //use Autofac as service provider
        .ConfigureServices((_, s) =>
        {
            s.AddSingleton<Random>(); //service collection stuff
        })
        .ConfigureContainer<ContainerBuilder>((_, b) =>
        {
            b.RegisterType<Random>().SingleInstance(); //Autofac stuff
        })
        .ConfigureLogging((_, _) =>
        {
            //add logging stuff here
        });
var host = hb.Build();

var sp = host.Services.GetRequiredService<IServiceProvider>();
_ = sp.GetService<IConfiguration>();
_ = sp.GetService<IHostEnvironment>();
_ = sp.GetService<ILogger<Program>>();
Console.ReadKey();


////run the host and return when done
//await hb.RunConsoleAsync();
