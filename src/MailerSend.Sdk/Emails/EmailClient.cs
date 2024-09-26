using System.Text;
using System.Text.Json;
using MailerSend.Sdk.Exceptions;

namespace MailerSend.Sdk.Emails;

public class EmailClient : MailerSendApi, IEmailClient
{
    public EmailClient(string apiKey, HttpClient? httpClient = null)
        : base(apiKey, httpClient)
    {
    }

    public async Task<EmailSendStatus> SendEmailAsync(SendEmailRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        request.Validate();

        var endpoint = "email";

        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(endpoint, content);

        return await ProcessSendEmailResponseAsync<EmailSendStatus>(response);
    }

    public async Task<BulkEmailResponse?> SendBulkEmailAsync(List<SendEmailRequest> request)
    {
        ArgumentNullException.ThrowIfNull(request);

        ValidateBulkEmail(request);
        
        var endpoint = "bulk-email";

        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(endpoint, content);

        return await ProcessResponseAsync<BulkEmailResponse>(response);
    }

    protected static void ValidateBulkEmail(List<SendEmailRequest> request) 
    {
        foreach (var email in request)
        {
            email.Validate();
        }
    }

    protected static async Task<EmailSendStatus> ProcessSendEmailResponseAsync<T>(HttpResponseMessage response)
    {
        var responseContent = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            if (response.Headers.Contains("x-send-paused") &&
                response.Headers.TryGetValues("x-send-paused", out var headerValues) &&
                bool.TryParse(headerValues.FirstOrDefault(), out var isPaused) && isPaused)
            {
                return EmailSendStatus.Paused;
            }

            return EmailSendStatus.Queued;
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
