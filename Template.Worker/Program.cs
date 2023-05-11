using Autofac;
using Autofac.Extensions.DependencyInjection;
using $ext_safeprojectname$.Core;
using $ext_safeprojectname$.Infrastructure;
using $ext_safeprojectname$.Worker;

IHost host = Host.CreateDefaultBuilder(args)
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterModule(new DefaultCoreModule());
        containerBuilder.RegisterModule(new DefaultInfrastructureModule(false));
    })
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddMassTransitConsumer<Consumer>("usuario", 5);
    })
    .Build();

await host.RunAsync();
