namespace MailerSend.Sdk.Domains;

public class DomainClient : MailerSendApi, IDomainsClient
{
    public DomainClient(string apiKey, HttpClient? httpClient = null)
            : base(apiKey, httpClient)
    {
    }

    public async Task<DomainListResponse?> GetDomainsAsync(DomainsListOptions? options)
    {
        var queryParams = BuildQueryParameters(options);
        var resource = "domains";
        var endpoint = QueryHelpers.AddQueryString(resource, queryParams);

        var response = await _httpClient.GetAsync(endpoint);

        return await ProcessResponseAsync<DomainListResponse>(response);
    }

    private static Dictionary<string, string?> BuildQueryParameters(DomainsListOptions? options)
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
}
