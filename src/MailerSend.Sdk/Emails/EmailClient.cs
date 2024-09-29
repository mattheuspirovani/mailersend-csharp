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

    public async Task<BulkEmailStatus?> GetBulkEmailStatusAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("Bulk Email ID cannot be null or empty.", nameof(id));

        var endpoint = $"bulk-email/{Uri.EscapeDataString(id)}";

        var response = await _httpClient.GetAsync(endpoint);

        var bulkEmailResponse = await ProcessResponseAsync<GetBulkEmailResponse>(response);

        return bulkEmailResponse?.Data;
    }

    public async Task<EmailVerificationResponse?> EmailVerificationAsync(EmailVerificationRequest emailVerificationRequest)
    {
        var endpoint = "email-verification/verify";
        var json = JsonSerializer.Serialize(emailVerificationRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(endpoint, content);

        return await ProcessResponseAsync<EmailVerificationResponse>(response);
    }

    public async Task<EmailVerificationListResponse?> GetAllEmailVerificationsAsync(int page = 1, int limit = 25)
    {
        var resource = "email-verification";
        var queryParameters = BuildQueryParameters(page, limit); 
        var endpoint = QueryHelpers.AddQueryString(resource, queryParameters);

        var response = await _httpClient.GetAsync(endpoint);

        return await ProcessResponseAsync<EmailVerificationListResponse>(response);
    }

    private static Dictionary<string, string?> BuildQueryParameters(int page = 1, int limit = 25)
    {
        var queryArguments = new Dictionary<string, string?>
        {
            { "page", page.ToString() },
            { "limit", limit.ToString() },
        };

        return queryArguments;
    }
}
