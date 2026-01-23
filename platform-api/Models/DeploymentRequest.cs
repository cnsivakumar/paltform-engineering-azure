namespace platform_api.Models;

public class DeploymentRequest
{
    public string appName { get; set; }
    public string appType { get; set; }
    public Repo readonlyepo { get; set; }
    public string deploymentTarget { get; set; }
    public string environment { get; set; }
    public string region { get; set; }
    public string expectedLoad { get; set; }
}

public class Repo
{
    public string provider { get; set; }
    public string url { get; set; }
    public string branch { get; set; }
}
