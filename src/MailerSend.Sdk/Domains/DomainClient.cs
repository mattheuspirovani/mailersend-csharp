using System.Text;
using System.Text.Json;

namespace MailerSend.Sdk.Domains;

public class DomainClient : MailerSendApi, IDomainsClient
{
    public DomainClient(string apiKey, HttpClient? httpClient = null)
            : base(apiKey, httpClient)
    {
    }

    public async Task<DomainListResponse?> GetDomainsAsync(DomainsListOptions? options)
    {
        var queryParams = BuildGetDomainQueryParameters(options);
        var resource = "domains";
        var endpoint = QueryHelpers.AddQueryString(resource, queryParams);

        var response = await _httpClient.GetAsync(endpoint);

        return await ProcessResponseAsync<DomainListResponse>(response);
    }

    private static Dictionary<string, string?> BuildGetDomainQueryParameters(DomainsListOptions? options)
    {
        var queryArguments = new Dictionary<string, string?>();

        if (options == null)
        {
            return queryArguments;
        }

        if (options.Page.HasValue)
            queryArguments.Add("page", options.Page.Value.ToString());

        if (options.Limit.HasValue)
            queryArguments.Add("limit", options.Limit.Value.ToString());


        if (options.Verified.HasValue)
            queryArguments.Add("verified", options.Verified.Value.ToString());

        return queryArguments;
    }

    public async Task<Domain?> GetDomainAsync(string domainId)
    {
        if (string.IsNullOrWhiteSpace(domainId))
            throw new ArgumentException("Domain ID cannot be null or empty.", nameof(domainId));

        var endpoint = $"domains/{Uri.EscapeDataString(domainId)}";

        var response = await _httpClient.GetAsync(endpoint);

        var domainResponse = await ProcessResponseAsync<DomainResponse>(response);

        return domainResponse?.Data;
    }

    public async Task<Domain?> CreateDomainAsync(CreateDomainRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("domains", content);
        var domainResponse = await ProcessResponseAsync<DomainResponse>(response);

        return domainResponse?.Data;
    }

    public async Task<bool> DeleteDomainAsync(string domainId)
    {
        if (string.IsNullOrWhiteSpace(domainId))
            throw new ArgumentException("Domain ID cannot be null or empty.", nameof(domainId));

        var endpoint = $"domains/{Uri.EscapeDataString(domainId)}";

        var response = await _httpClient.DeleteAsync(endpoint);

        return response.IsSuccessStatusCode;
    }

    public async Task<RecipientListResponse?> GetRecipientsAsync(string domainId, RecipientListOptions? options = null)
    {
        if (string.IsNullOrWhiteSpace(domainId))
            throw new ArgumentException("Domain ID cannot be null or empty.", nameof(domainId));

        var resource = $"domains/{Uri.EscapeDataString(domainId)}/recipients";
        var endpoint = BuildGetRecipientQueryParams(resource, options);

        var response = await _httpClient.GetAsync(endpoint);

        var recipientsResponse = await ProcessResponseAsync<RecipientListResponse>(response);

        return recipientsResponse;
    }

    private string BuildGetRecipientQueryParams(string baseUrl, RecipientListOptions? options)
    {
        var queryParams = new Dictionary<string, string?>();

        if (options != null)
        {
            if (options.Page.HasValue)
            {
                queryParams.Add("page", options.Page.Value.ToString());
            }

            if (options.Limit.HasValue)
            {
                queryParams.Add("limit", options.Limit.Value.ToString());
            }
        }

        if (queryParams.Count > 0)
        {
            baseUrl = QueryHelpers.AddQueryString(baseUrl, queryParams);
        }

        return baseUrl;
    }
}
