using System.Net;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using platform_api.Models;

namespace platform_api.Functions;

public class DeployFunction
{
    private readonly ILogger _logger;
    private readonly ServiceBusClient _serviceBusClient;

    public DeployFunction(
        ILoggerFactory loggerFactory,
        ServiceBusClient serviceBusClient)
    {
        _logger = loggerFactory.CreateLogger<DeployFunction>();
        _serviceBusClient = serviceBusClient;
    }

    [Function("DeployFunction")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")]
        HttpRequestData req)
    {
        _logger.LogInformation("DeployFunction triggered");

        var body = await new StreamReader(req.Body).ReadToEndAsync();
        var request = JsonSerializer.Deserialize<DeploymentRequest>(body);

        if (request == null)
        {
            var bad = req.CreateResponse(HttpStatusCode.BadRequest);
            await bad.WriteStringAsync("Invalid request body");
            return bad;
        }

        // Send message to Service Bus
        var sender = _serviceBusClient.CreateSender("deployment-requests");
        await sender.SendMessageAsync(
            new ServiceBusMessage(JsonSerializer.Serialize(request)));

        var response = req.CreateResponse(HttpStatusCode.Accepted);
        await response.WriteAsJsonAsync(new
        {
            message = "Deployment request accepted",
            appName = request.appName
        });

        return response;
    }
}
