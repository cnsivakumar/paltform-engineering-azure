namespace PlatformApi.Models
{
    public class DeploymentRequest
    {
        public string RequestId { get; set; } = Guid.NewGuid().ToString();
        public string AppName { get; set; } = string.Empty;
        public string AppType { get; set; } = string.Empty; // dotnet/java
        public string RepoUrl { get; set; } = string.Empty;
        public string RepoBranch { get; set; } = "main";
        public string DeploymentTarget { get; set; } = "webapp"; // vm/aks/webapp
        public string Environment { get; set; } = "dev";
        public string Region { get; set; } = "eastus";
        public string ExpectedLoad { get; set; } = "low";
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
