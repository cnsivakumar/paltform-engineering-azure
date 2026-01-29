using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.ServiceBus;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using platform_api.Models;
namespace platform_api.Functions;

public class DeployWorkerFunction
{
    private readonly ILogger _logger;
    private readonly GitHubActionsService _gitHub;

    public DeployWorkerFunction(ILoggerFactory loggerFactory, GitHubActionsService gitHub)
    {
        _logger = loggerFactory.CreateLogger<DeployWorkerFunction>();
        _gitHub = gitHub;
    }

    [Function("DeploymentWorker")]
    public async Task Run(
        [ServiceBusTrigger(
            "deployment-requests",
            Connection = "ServiceBusConnection")]
        string message)
    {
        var request = JsonSerializer.Deserialize<DeploymentRequest>(message);

        if (request == null)
        {
            _logger.LogError("Invalid message payload");
            return;
        }

        _logger.LogInformation("Triggering GitHub Actions pipeline");

        await _gitHub.TriggerWorkflowAsync(
            Environment.GetEnvironmentVariable("GITHUB_OWNER")!,
            Environment.GetEnvironmentVariable("GITHUB_REPO")!,
            Environment.GetEnvironmentVariable("GITHUB_TOKEN")!,
            request.AppName,
            request.Environment,
            request.DeploymentTarget
        );

        _logger.LogInformation("GitHub workflow triggered successfully");
    }
}
