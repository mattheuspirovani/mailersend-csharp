using System.Text;
using System.Text.Json;

namespace MailerSend.Sdk.Emails;

public class EmailClient : MailerSendApi, IEmailClient
{
    public EmailClient(string apiKey, HttpClient? httpClient = null)
        : base(apiKey, httpClient)
    {
    }

    public async Task SendEmailAsync(SendEmailRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var endpoint = "email";

        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(endpoint, content);
    }
}
