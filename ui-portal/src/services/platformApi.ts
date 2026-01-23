import type { DeploymentRequest } from "../models/DeploymentRequest";


const FUNCTION_URL = import.meta.env.VITE_DEPLOY_API_URL;


export async function deployApp(request: DeploymentRequest) {
  const response = await fetch(
    `${FUNCTION_URL}`,
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
