using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace platform_api.Functions;

public class DeployWorkerFunction
{
    private readonly ILogger _logger;

    public DeployWorkerFunction(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<DeployWorkerFunction>();
    }

    [Function("DeploymentWorker")]
    public void Run(
        [ServiceBusTrigger(
            "%DeploymentQueueName%",
            Connection = "ServiceBusConnection")]
        string message)
    {
        _logger.LogInformation($"Processing deployment: {message}");
    }
}
