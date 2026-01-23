using platform_api.Models;
namespace platform_api.Services;

public class DeploymentDecisionService : IDeploymentDecisionService
{
    public DeploymentPlan CreatePlan(DeploymentRequest request)
    {
        var plan = new DeploymentPlan
        {
            appName = request.appName,
            deploymentTarget = request.deploymentTarget
        };

        switch (request.deploymentTarget.ToLower())
        {
            case "webapp":
                plan.computeType = "Azure App Service";
                plan.suggestedSku = "B1";
                plan.scalingStrategy = "Auto-scale (2-5 instances)";
                break;

            case "vm":
                plan.computeType = "Azure Virtual Machine";
                plan.suggestedSku = "Standard_D2s_v5";
                plan.scalingStrategy = "Manual scale / VMSS (future)";
                break;

            case "aks":
                plan.computeType = "Azure Kubernetes Service";
                plan.suggestedSku = "Standard_DS2_v2 (node)";
                plan.scalingStrategy = "HPA + Cluster Autoscaler";
                break;

            default:
                plan.computeType = "Unknown";
                plan.scalingStrategy = "N/A";
                plan.status = "Rejected";
                break;
        }

        return plan;
    }
}
