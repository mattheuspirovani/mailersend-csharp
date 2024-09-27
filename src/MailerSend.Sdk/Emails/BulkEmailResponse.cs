using System.Text.Json.Serialization;

namespace MailerSend.Sdk.Emails;

public class BulkEmailResponse
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = null!;
    [JsonPropertyName("bulk_email_id")]
    public string BulkEmailId { get; set; } = null!;
}
