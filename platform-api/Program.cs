using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using platform_api.Services;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        // App services
        services.AddSingleton<IDeploymentDecisionService, DeploymentDecisionService>();
        services.AddSingleton<GitHubActionsService>();

        // Service Bus client
        services.AddSingleton(sp =>
        {
            var connectionString = Environment.GetEnvironmentVariable("ServiceBusConnection");
            return new ServiceBusClient(connectionString);
        });
    })
    .Build();

host.Run();
