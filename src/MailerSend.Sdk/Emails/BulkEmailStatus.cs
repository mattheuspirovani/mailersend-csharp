using System.Text.Json.Serialization;

namespace MailerSend.Sdk.Emails;

public class BulkEmailStatus
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = null!;

    [JsonPropertyName("state")]
    public string State { get; set; } = null!;

    [JsonPropertyName("total_recipients_count")]
    public int TotalRecipientsCount { get; set; }

    [JsonPropertyName("suppressed_recipients_count")]
    public int SuppressedRecipientsCount { get; set; }

    [JsonPropertyName("suppressed_recipients")]
    public List<string>? SuppressedRecipients { get; set; } //TODO Assuming suppressed_recipients is a list

    [JsonPropertyName("validation_errors_count")]
    public int ValidationErrorsCount { get; set; }

    [JsonPropertyName("validation_errors")]
    public List<string>? ValidationErrors { get; set; } //TODO Assuming validation_errors is a list

    [JsonPropertyName("messages_id")]
    public List<string> MessagesId { get; set; }  // Converted from string to list

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }
}
