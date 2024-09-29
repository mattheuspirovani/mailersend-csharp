using System;
using System.Text;
using System.Text.Json;
using MailerSend.Sdk.Exceptions;

namespace MailerSend.Sdk.Sms;

public class SmsClient: MailerSendApi, ISmsClient
{
    public SmsClient(string apiKey, HttpClient? httpClient = null) : base(apiKey, httpClient)
    {
    }

    public async Task<SmsResponse> SendSmsAsync(SmsRequest smsRequest)
    {
        var endpoint = "sms";
        var json = JsonSerializer.Serialize(smsRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(endpoint, content);

        return await ProcessSendSmsResponseAsync<SmsResponse>(response);        
    }

    protected static async Task<SmsResponse> ProcessSendSmsResponseAsync<T>(HttpResponseMessage response)
    {
        var responseContent = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var messageId = response.Headers.GetValues("X-SMS-Message-Id").FirstOrDefault();

            if (response.Headers.Contains("X-SMS-Send-Paused") &&
                response.Headers.TryGetValues("X-SMS-Send-Paused", out var headerValues) &&
                bool.TryParse(headerValues.FirstOrDefault(), out var isPaused) && isPaused)
            {
                return new SmsResponse
                {
                    MessageId = messageId!,
                    IsPaused = isPaused
                };
            }

            return new SmsResponse
            {
                MessageId = messageId!,
                IsPaused = false
            };
        }
        else
        {
            ApiErrorResponse? errorResponse = null;

            try
            {
                errorResponse = JsonSerializer.Deserialize<ApiErrorResponse>(responseContent, _propertyNameCaseInsensitive);
            }
            catch
            {
                throw new MailerSendException("Failed to process exception response", response.StatusCode);
            }

            var errorMessage = errorResponse?.Message ?? $"API request failed with status code {(int)response.StatusCode}.";

            throw new MailerSendException(errorMessage, response.StatusCode, errorResponse?.Message, errorResponse?.Errors);
        }
    }
}
