using platform_api.Models;

namespace platform_api.Services;

public interface IDeploymentDecisionService
{
    DeploymentPlan CreatePlan(DeploymentRequest request);
}
