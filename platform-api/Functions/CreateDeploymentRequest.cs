using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using PlatformApi.Models;
using PlatformApi.Services;
using System.Net;
using System.Text.Json;

namespace PlatformApi.Functions
{
    public class CreateDeploymentRequest
    {
        private readonly DeploymentService _service;

        public CreateDeploymentRequest(DeploymentService service)
        {
            _service = service;
        }

        [Function("CreateDeploymentRequest")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "deployments")] HttpRequestData req)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var deploymentRequest = JsonSerializer.Deserialize<DeploymentRequest>(requestBody);

            if (deploymentRequest == null)
            {
                var badResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await badResponse.WriteStringAsync("Invalid deployment request");
                return badResponse;
            }

            var created = _service.CreateDeployment(deploymentRequest);
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json");
            await response.WriteStringAsync(JsonSerializer.Serialize(created));
            return response;
        }
    }
}
