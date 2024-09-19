using System;
using System.Net.Http.Headers;

namespace MailerSend.Sdk;

public class MailerSendApi
{
    protected readonly HttpClient _httpClient;

    public MailerSendApi(string apiKey, HttpClient? httpClient = null)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            throw new ArgumentException("API Key its mandatory.", nameof(apiKey));
        }

        _httpClient = httpClient ?? new HttpClient
        {
            BaseAddress = new Uri("https://api.mailersend.com/v1/")
        };

        if (!_httpClient.DefaultRequestHeaders.Contains("Authorization"))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        }

        if (!_httpClient.DefaultRequestHeaders.Accept.Any(h => h.MediaType == "application/json"))
        {
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
