using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using platform_api.Services;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
    

        // App services
        services.AddSingleton<IDeploymentDecisionService, DeploymentDecisionService>();

        // Service Bus client
        services.AddSingleton(sp =>
            new ServiceBusClient(
                Environment.GetEnvironmentVariable("ServiceBusConnection")));
    })
    .Build();

host.Run();
