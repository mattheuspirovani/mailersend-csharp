using System;
using System.Net.Http.Headers;
using System.Text.Json;
using MailerSend.Sdk.Exceptions;

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

    protected static async Task<T?> ProcessResponseAsync<T>(HttpResponseMessage response)
    {
        var responseContent = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            if (typeof(T) == typeof(string))
            {
                return (T)(object)responseContent;
            }
            else if (typeof(T) == typeof(VoidType))
            {                
                return default;
            }
            else
            {
                return JsonSerializer.Deserialize<T>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }
        else
        {
            ApiErrorResponse? errorResponse = null;

            try
            {
                errorResponse = JsonSerializer.Deserialize<ApiErrorResponse>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch
            {
            }

            var errorMessage = errorResponse?.Message ?? $"API request failed with status code {(int)response.StatusCode}.";

            throw new MailerSendException(errorMessage, response.StatusCode, errorResponse?.Message, errorResponse?.Errors);
        }
    }
}
public class VoidType { }

