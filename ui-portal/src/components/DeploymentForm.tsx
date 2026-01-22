import { useState } from "react";
import { createDeployment } from "../services/platformApi";
import type { DeploymentRequest } from "../models/DeploymentRequest";

export default function DeploymentForm() {
  const [status, setStatus] = useState<string>("");

  const [form, setForm] = useState<DeploymentRequest>({
    appName: "",
    appType: "dotnet",
    repo: {
      provider: "github",
      url: "",
      branch: "main"
    },
    deploymentTarget: "webapp",
    environment: "dev",
    region: "eastus",
    expectedLoad: "low"
  });

  const handleChange = (e: any) => {
    const { name, value } = e.target;

    if (name.startsWith("repo.")) {
      setForm({
        ...form,
        repo: {
          ...form.repo,
          [name.split(".")[1]]: value
        }
      });
    } else {
      setForm({ ...form, [name]: value });
    }
  };

  const submit = async () => {
    try {
      const result = await createDeployment(form);
      setStatus(`Request submitted. ID: ${result.requestId}`);
    } catch (err: any) {
      setStatus(err.message);
    }
  };

  return (
    <div style={{ maxWidth: "600px" }}>
      <h2>New Deployment Request</h2>

      <input name="appName" placeholder="App Name" onChange={handleChange} />
      <br /><br />

      <input name="repo.url" placeholder="GitHub Repo URL" onChange={handleChange} />
      <br /><br />

      <input name="repo.branch" placeholder="Branch" onChange={handleChange} />
      <br /><br />

      <select name="deploymentTarget" onChange={handleChange}>
        <option value="webapp">Web App</option>
        <option value="vm">Virtual Machine</option>
        <option value="aks">AKS</option>
      </select>
      <br /><br />

      <select name="environment" onChange={handleChange}>
        <option value="dev">Dev</option>
        <option value="qa">QA</option>
      </select>
      <br /><br />

      <button onClick={submit}>Submit</button>

      <p>{status}</p>
    </div>
  );
}
