using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using platform_api.Models;
namespace platform_api.Functions;

public class DeployWorkerFunction
{
    private readonly ILogger _logger;

    public DeployWorkerFunction(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<DeployWorkerFunction>();
    }

    [Function("DeploymentWorker")]
    public async Task Run(
        [ServiceBusTrigger(
            "deployment-requests",
            Connection = "ServiceBusConnection")]
        string message)
    {
        _logger.LogInformation("Received deployment message");
        _logger.LogInformation("Message content: {Message}", message);

        var request = JsonSerializer.Deserialize<DeploymentRequest>(
            message,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (request == null)
        {
            _logger.LogError("Failed to deserialize deployment request");
            return;
        }

        _logger.LogInformation(
            "Processing deployment for App={AppName}, Env={Env}, Target={Target}",
            request.AppName,
            request.Environment,
            request.DeploymentTarget
        );

        // Next step: trigger Azure DevOps / GitHub Actions pipeline
        await Task.CompletedTask;
    }
}
