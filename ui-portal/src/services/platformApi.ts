import type { DeploymentRequest } from "../models/DeploymentRequest";

export async function deployApp(request: DeploymentRequest) {
  const response = await fetch(
   "/api/DeployFunction",
    {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(request),
    }
  );

  if (!response.ok) {
    throw new Error("Deployment request failed");
  }

  return response.json();
}
