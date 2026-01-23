namespace platform_api.Models;

public class DeploymentRequest
{
    public string appName { get; set; } = "";
    public string appType { get; set; } = ""; // dotnet | java
    public string deploymentTarget { get; set; } = ""; // vm | aks | webapp
    public string environment { get; set; } = ""; // dev | test | prod
}
