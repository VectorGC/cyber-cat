using Shared.Server.Configurations;
using Shared.Server.Domain;
using Shared.Server.Infrastructure;

namespace TaskService.Infrastructure;

public class SharedTaskWebHookProcessor
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<SharedTaskWebHookProcessor> _logger;

    public SharedTaskWebHookProcessor(IHttpClientFactory httpClientFactory, ILogger<SharedTaskWebHookProcessor> logger)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<WebHookResultStatus> ProcessWebHook(SharedTaskProgress sharedTaskProgress)
    {
        if (sharedTaskProgress == null)
            return null;

        var dto = new SharedTaskExternalDto(sharedTaskProgress);
        return await ProcessWebHook(dto);
    }

    public async Task<WebHookResultStatus> ProcessWebHook(SharedTaskExternalDto dto)
    {
        if (dto == null)
            return null;

        var client = _httpClientFactory.CreateClient();
        var config = await client.GetFromJsonAsync<CyberCatExternalConfig>("https://api.npoint.io/0deb88d098e7e796a8df");
        if (string.IsNullOrEmpty(config?.SharedTasksWebHook))
        {
            _logger.LogInformation("Not found web hook endpoint");
            return new WebHookResultStatus()
            {
                Error = "Not found web hook endpoint"
            };
        }

        var postResponse = await client.PostAsJsonAsync(config.SharedTasksWebHook, dto);

        try
        {
            postResponse.EnsureSuccessStatusCode();
            _logger.LogInformation("Success web hook processed for task {Task}", dto.TaskId);

            return new WebHookResultStatus()
            {
                WebHook = config.SharedTasksWebHook,
                Model = dto
            };
        }
        catch (Exception e)
        {
            _logger.LogError("{Exception}", e);
            return new WebHookResultStatus()
            {
                WebHook = config.SharedTasksWebHook,
                Model = dto,
                Error = e.ToString()
            };
        }
    }
}