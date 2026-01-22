using System.Net;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using platform_api.Models;

namespace platform_api;

public class DeployFunction
{
    private readonly ILogger<DeployFunction> _logger;

    public DeployFunction(ILogger<DeployFunction> logger)
    {
        _logger = logger;
    }

    [Function("DeployFunction")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
    {
        _logger.LogInformation("Deploy function triggered.");

        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var request = JsonSerializer.Deserialize<DeploymentRequest>(requestBody);

        _logger.LogInformation($"Deployment request for {request?.AppName}");

        string decision = request?.DeploymentTarget switch
        {
            "vm" => "Deploying to Azure Virtual Machine",
            "aks" => "Deploying to Azure Kubernetes Service",
            "webapp" => "Deploying to Azure App Service",
            _ => "Invalid deployment target"
        };

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(new
        {
            message = decision,
            status = "Accepted"
        });

        return response;
    }
}
