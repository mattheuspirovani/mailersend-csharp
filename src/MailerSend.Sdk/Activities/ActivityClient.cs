using System.Text.Json;
using MailerSend.Sdk.Exceptions;

namespace MailerSend.Sdk.Activities;

public class ActivityClient : MailerSendApi, IActivityClient
{

    public ActivityClient(string apiKey, HttpClient? httpClient = null) : base(apiKey, httpClient)
    {

    }

    public async Task<ActivityListResponse?> GetActivitiesAsync(string domain, ActivityListOptions options)
    {
        var queryParams = BuildQueryParameters(options);
        var resource = $"activity/{Uri.EscapeDataString(domain)}";
        var endpoint = QueryHelpers.AddQueryString(resource, queryParams);

        var response = await _httpClient.GetAsync(endpoint);

        return await ProcessResponseAsync<ActivityListResponse>(response);
    }

    private static Dictionary<string, string?> BuildQueryParameters(ActivityListOptions options)
    {
        var queryArguments = new Dictionary<string, string?>();

        if (!string.IsNullOrEmpty(options.Event))
            queryArguments.Add("event", Uri.EscapeDataString(options.Event));

        queryArguments.Add("date_from", new DateTimeOffset(options.DateFrom).ToUnixTimeSeconds().ToString());
        queryArguments.Add("date_to", new DateTimeOffset(options.DateTo).ToUnixTimeSeconds().ToString());

        if (options.Page.HasValue)
            queryArguments.Add("page", options.Page.Value.ToString());

        if (options.Limit.HasValue)
            queryArguments.Add("limit", options.Limit.Value.ToString());
        
        return queryArguments;
    }

    public async Task<Activity?> GetActivityAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Activity ID cannot be null or empty.", nameof(id));
        }

        var endpoint = $"activity/{Uri.EscapeDataString(id)}";

        var response = await _httpClient.GetAsync(endpoint);

        return await ProcessResponseAsync<Activity>(response);
    }
}
