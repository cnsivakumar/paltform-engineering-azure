export interface DeploymentRequest {
  appName: string;
  appType: string;
  repo: {
    provider: string;
    url: string;
    branch: string;
  };
  deploymentTarget: string;
  environment: string;
  region: string;
  expectedLoad: string;
}
