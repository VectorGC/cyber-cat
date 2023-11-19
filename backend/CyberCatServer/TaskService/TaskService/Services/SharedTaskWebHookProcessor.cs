using Shared.Server.Data;

namespace TaskService.Services;

public class SharedTaskWebHookProcessor
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<SharedTaskWebHookProcessor> _logger;

    public SharedTaskWebHookProcessor(IHttpClientFactory httpClientFactory, ILogger<SharedTaskWebHookProcessor> logger)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    public async Task ProcessWebHook(SharedTaskProgressData sharedTaskProgress)
    {
        if (sharedTaskProgress == null)
            return;

        var client = _httpClientFactory.CreateClient();
        var config = await client.GetFromJsonAsync<CyberCatExternalConfig>("https://api.npoint.io/a5053a9ffd7df38ecbd4");
        if (string.IsNullOrEmpty(config?.SharedTasksWebHook))
        {
            _logger.LogInformation("Not found web hook endpoint");
            return;
        }

        var dto = new SharedTaskExternalDto(sharedTaskProgress);
        var postResponse = await client.PostAsJsonAsync(config.SharedTasksWebHook, dto);

        try
        {
            postResponse.EnsureSuccessStatusCode();
            _logger.LogInformation("Success web hook processed for task {Task}", sharedTaskProgress.Id);
        }
        catch (Exception e)
        {
            _logger.LogError("{Exception}", e);
        }
    }
}