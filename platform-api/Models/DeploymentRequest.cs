namespace platform_api.Models;

public class DeploymentRequest
{
    public string AppName { get; set; } = "";
    public string AppType { get; set; } = ""; // dotnet | java
    public string DeploymentTarget { get; set; } = ""; // vm | aks | webapp
    public string Environment { get; set; } = ""; // dev | test | prod
}
