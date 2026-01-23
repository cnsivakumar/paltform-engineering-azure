using System.Net;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using platform_api.Models;
using platform_api.Services;

namespace platform_api;

public class DeployFunction
{
    private readonly ILogger<DeployFunction> _logger;
    private readonly IDeploymentDecisionService _decisionService;

    public DeployFunction(ILogger<DeployFunction> logger,IDeploymentDecisionService decisionService)
    {
        _logger = logger;
        _decisionService = decisionService;
    }

    [Function("DeployFunction")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")]
        HttpRequest req,
        [ServiceBus(
            "%DeploymentQueueName%",
            Connection = "ServiceBusConnection")]
        IAsyncCollector<string> queueCollector,
        ILogger log)
    {   
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();

        await queueCollector.AddAsync(requestBody);

        return new OkObjectResult(new
        {
            message = "Deployment request accepted",
            status = "Queued"
        });
    }

}
