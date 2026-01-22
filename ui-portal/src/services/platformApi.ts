import type { DeploymentRequest } from "../models/DeploymentRequest";

export async function createDeployment(request: DeploymentRequest) {
  const response = await fetch("/api/deployments", {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify(request)
  });

  if (!response.ok) {
    throw new Error("Failed to submit deployment request");
  }

  return response.json();
}
