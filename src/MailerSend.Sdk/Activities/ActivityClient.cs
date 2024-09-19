using System.Text.Json;
using MailerSend.Sdk.Exceptions;

namespace MailerSend.Sdk.Activities;

public class ActivityClient : MailerSendApi, IActivityClient
{

    public ActivityClient(string apiKey, HttpClient? httpClient = null) : base(apiKey, httpClient)
    {
        
    }

    private static readonly JsonSerializerOptions s_PropertyNameCaseInsensitive = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task<ActivityListResponse?> GetActivitiesAsync(ActivityListOptions? options)
    {
        var queryParams = BuildQueryParameters(options);
        var endpoint = "activity";

        if (!string.IsNullOrEmpty(queryParams))
        {
            endpoint += $"?{queryParams}";
        }

        var response = await _httpClient.GetAsync(endpoint);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new MailerSendException($"Failed to retrieve activities: {content}", response.StatusCode);
        }

        var activityList = JsonSerializer.Deserialize<ActivityListResponse>(content, s_PropertyNameCaseInsensitive);

        return activityList;
    }

    private static string BuildQueryParameters(ActivityListOptions? options)
    {
        if (options == null)
            return string.Empty;

        var parameters = new List<string>();

        if (!string.IsNullOrEmpty(options.DomainId))
            parameters.Add($"domain_id={Uri.EscapeDataString(options.DomainId)}");

        if (!string.IsNullOrEmpty(options.Event))
            parameters.Add($"event={Uri.EscapeDataString(options.Event)}");

        if (!string.IsNullOrEmpty(options.Recipient))
            parameters.Add($"recipient={Uri.EscapeDataString(options.Recipient)}");

        if (!string.IsNullOrEmpty(options.MessageId))
            parameters.Add($"message_id={Uri.EscapeDataString(options.MessageId)}");

        if (options.DateFrom.HasValue)
            parameters.Add($"date_from={new DateTimeOffset(options.DateFrom.Value).ToUnixTimeSeconds()}");

        if (options.DateTo.HasValue)
            parameters.Add($"date_to={new DateTimeOffset(options.DateTo.Value).ToUnixTimeSeconds()}");

        if (options.Page.HasValue)
            parameters.Add($"page={options.Page.Value}");

        if (options.Limit.HasValue)
            parameters.Add($"limit={options.Limit.Value}");

        return string.Join("&", parameters);
    }

    public async Task<Activity?> GetActivityAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Activity ID cannot be null or empty.", nameof(id));
        }

        var endpoint = $"activity/{Uri.EscapeDataString(id)}";

        var response = await _httpClient.GetAsync(endpoint);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new MailerSendException($"Failed to retrieve activity: {content}", response.StatusCode);
        }

        var activity = JsonSerializer.Deserialize<Activity>(content, s_PropertyNameCaseInsensitive);

        return activity;
    }
}
