using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace platform_api.Services;

public class GitHubActionsService
{
    private readonly HttpClient _httpClient = new();

    public async Task TriggerWorkflowAsync(
        string owner,
        string repo,
        string token,
        string appName,
        string environment,
        string target)
    {
        var url =
            $"https://api.github.com/repos/{owner}/{repo}/actions/workflows/deploy-app.yml/dispatches";

        var payload = new
        {
            @ref = "main",
            inputs = new
            {
                appName,
                environment,
                target
            }
        };

        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        request.Headers.Add("User-Agent", "platform-api");
        request.Content = new StringContent(
            JsonSerializer.Serialize(payload),
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.SendAsync(request);

        response.EnsureSuccessStatusCode();
    }
}
