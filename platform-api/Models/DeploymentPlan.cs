namespace platform_api.Models;

public class DeploymentPlan
{
    public string requestId { get; set; } = Guid.NewGuid().ToString();
    public string appName { get; set; } = string.Empty;
    public string deploymentTarget { get; set; } = string.Empty;

    public string computeType { get; set; } = string.Empty;   // VM / AKS / WebApp
    public string suggestedSku { get; set; } = string.Empty;  // B1, D2s_v5, etc
    public string scalingStrategy { get; set; } = string.Empty;
    public string estimatedCost { get; set; } = "TBD";

    public string status { get; set; } = "Planned";
}
